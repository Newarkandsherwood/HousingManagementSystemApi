namespace HousingManagementSystemApi.Tests.ServiceTests;

using System;
using System.IO;
using System.Net.Mime;
using System.Text;
using System.Xml.Serialization;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Models.Capita;
using Moq;
using RestSharp;
using RichardSzalay.MockHttp;
using Services;
using Xunit;

public class CapitaServiceTests : IDisposable
{
    private RestClient restClient;
    private readonly CapitaService systemUnderTest;
    private readonly MockHttpMessageHandler mockHttpMessageHandler = new();
    private const string LocationId = "locationId";
    private const string SorCode = "SOR_CODE";
    private const string WorkOrderId = "WorkOrderID";
    private const string Description = "description";
    private const string CaptiaUrl = "http://test.com";

    public CapitaServiceTests()
    {
        var capitaOptionsMock = new Mock<IOptions<CapitaOptions>>();
        capitaOptionsMock.Setup(x => x.Value)
            .Returns(new CapitaOptions
            {
                ApiAddress = new Uri(CaptiaUrl),
                Username = "username",
                Password = "password",
            });
        restClient = new RestClient(new RestClientOptions
        {
            ConfigureMessageHandler = _ => mockHttpMessageHandler,
            BaseUrl = new Uri(CaptiaUrl)
        });
        systemUnderTest = new CapitaService(restClient, capitaOptionsMock.Object);
    }

    [Fact]
#pragma warning disable CA1707
    public async void GivenValidParameters_WhenLoggingJobSucceeds_ThenWorkOrderIdReturned()
#pragma warning restore CA1707
    {
        // Arrange
        var logJobResponse =
            new LogJobResponse { Jobs = new Jobs { Job_logged = new Job_logged { Job_no = WorkOrderId, Logged_info = "logged info" } } };
        var content = ToXml(logJobResponse);
        mockHttpMessageHandler.When($"{CaptiaUrl}/*").Respond(MediaTypeNames.Application.Xml, content);

        // Act
        var actual = await systemUnderTest.LogJob(Description, LocationId, SorCode);

        // Assert
        actual.Should().Be(WorkOrderId);
    }

    [Fact]
#pragma warning disable CA1707
    public async void GivenARemoteCallFailed_WhenLoggingJob_ThenExceptionIsThrown()
#pragma warning restore CA1707
    {
        // Arrange
        mockHttpMessageHandler.When($"{CaptiaUrl}/*").Respond(() => throw new());

        // Act
        var act = async () => await systemUnderTest.LogJob(Description, $"{LocationId}2", SorCode);

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }

    [Fact]
#pragma warning disable CA1707
    public async void GivenLogJobResponseContainsErrorDetails_WhenLoggingJob_ThenExceptionIsThrown()
#pragma warning restore CA1707
    {
        // Arrange
        var logJobResponse =
            new LogJobResponse { ErrorDetails = "An Error" };
        var content = ToXml(logJobResponse);
        mockHttpMessageHandler.When($"{CaptiaUrl}/*").Respond(MediaTypeNames.Application.Xml, content);

        // Act
        var act = async () => await systemUnderTest.LogJob(Description, $"{LocationId}2", SorCode);

        // Assert
        await act.Should().ThrowExactlyAsync<SystemException>();
    }

    [Fact]
#pragma warning disable CA1707
    public async void GivenValidParameters_WhenLoggingJob_ThenRequestContainsHeaderWithSecurityCredentials()
#pragma warning restore CA1707
    {
        // Arrange
        var logJobResponse =
            new LogJobResponse { Jobs = new Jobs { Job_logged = new Job_logged { Job_no = WorkOrderId, Logged_info = "logged info" } } };
        var content = ToXml(logJobResponse);
        mockHttpMessageHandler.When($"{CaptiaUrl}/*")
            .WithPartialContent("Header")
            .WithPartialContent("<Security username=")
            .WithPartialContent("password=")
            .Respond(MediaTypeNames.Application.Xml, content);

        // Act
        var act = async () => await systemUnderTest.LogJob(Description, LocationId, SorCode);

        // Assert
        await act.Should().NotThrowAsync();
    }

    [Theory]
    [MemberData(nameof(InvalidArgumentTestData))]
#pragma warning disable xUnit1026
#pragma warning disable CA1707
    public async void GivenAnInvalidDescription_WhenLoggingJob_ThenExceptionIsThrown<T>(T exception,
        string description) where T : Exception
#pragma warning restore CA1707
#pragma warning restore xUnit1026
    {
        // Arrange

        // Act
        var act = async () => await systemUnderTest.LogJob(description, LocationId, SorCode);

        // Assert
        await act.Should().ThrowExactlyAsync<T>();
    }

    [Theory]
    [MemberData(nameof(InvalidArgumentTestData))]
#pragma warning disable xUnit1026
#pragma warning disable CA1707
    public async void GivenAnInvalidLocationId_WhenLoggingJob_ThenExceptionIsThrown<T>(T exception,
        string locationId) where T : Exception
#pragma warning restore CA1707
#pragma warning restore xUnit1026
    {
        // Arrange

        // Act
        var act = async () => await systemUnderTest.LogJob(Description, locationId, SorCode);

        // Assert
        await act.Should().ThrowExactlyAsync<T>();
    }

    [Theory]
    [MemberData(nameof(InvalidArgumentTestData))]
#pragma warning disable xUnit1026
#pragma warning disable CA1707
    public async void GivenAnInvalidSorCode_WhenLoggingJob_ThenExceptionIsThrown<T>(T exception,
        string sorCode) where T : Exception
#pragma warning restore CA1707
#pragma warning restore xUnit1026
    {
        // Arrange

        // Act
        var act = async () => await systemUnderTest.LogJob(Description, LocationId, sorCode);

        // Assert
        await act.Should().ThrowExactlyAsync<T>();
    }

    public static TheoryData<ArgumentException, string> InvalidArgumentTestData() => new()
    {
        { new ArgumentNullException(), null },
        { new ArgumentException(), "" },
        { new ArgumentException(), " " },
    };

    private static string ToXml<T>(T obj)
    {
        using (var stringWriter = new Utf8StringWriter())
        {
            var xmlSerializerNamespaces = new XmlSerializerNamespaces();
            xmlSerializerNamespaces.Add(string.Empty, string.Empty);

            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stringWriter, obj, xmlSerializerNamespaces);

            return stringWriter.ToString();
        }
    }

    private sealed class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }

    public void Dispose()
    {
        this.restClient?.Dispose();
        this.mockHttpMessageHandler?.Dispose();
    }
}
