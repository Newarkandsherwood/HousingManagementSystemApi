namespace HousingManagementSystemApi.Tests.UseCasesTests;

using System;
using FluentAssertions;
using Gateways;
using Moq;
using UseCases;
using Xunit;

public class CreateWorkOrderUseCaseTests
{
    private readonly CreateWorkOrderUseCase systemUnderTest;
    private const string LocationId = "locationId";
    private const string SorCode = "SOR_CODE";
    private const string WorkOrderId = "test";
    private const string Description = "description";

    private Mock<IWorkOrderGateway> workOrderGatewayMock;

    public CreateWorkOrderUseCaseTests()
    {
        workOrderGatewayMock = new Mock<IWorkOrderGateway>();
        systemUnderTest = new CreateWorkOrderUseCase(workOrderGatewayMock.Object);
    }

    [Fact]
    public async void GivenValidParameters_WhenExecuting_ThenWorkOrderGatewayCreateWorkOrderIsCalled()
    {
        // Arrange
        workOrderGatewayMock.Setup(gateway => gateway.CreateWorkOrder(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(WorkOrderId);

        // Act
        await systemUnderTest.Execute(Description, LocationId, SorCode);

        // Assert
        workOrderGatewayMock.VerifyAll();
    }

    [Theory]
    [MemberData(nameof(InvalidArgumentTestData))]
#pragma warning disable xUnit1026
#pragma warning disable CA1707
    public async void GivenAnInvalidDescription_WhenExecuting_ThenExceptionIsThrown<T>(T exception,
        string description) where T : Exception
#pragma warning restore CA1707
#pragma warning restore xUnit1026
    {
        // Arrange

        // Act
        var act = async () => await systemUnderTest.Execute(description, LocationId, SorCode);

        // Assert
        await act.Should().ThrowExactlyAsync<T>();
    }

    [Theory]
    [MemberData(nameof(InvalidArgumentTestData))]
#pragma warning disable xUnit1026
#pragma warning disable CA1707
    public async void GivenAnInvalidLocationId_WhenExecuting_ThenExceptionIsThrown<T>(T exception,
        string locationId) where T : Exception
#pragma warning restore CA1707
#pragma warning restore xUnit1026
    {
        // Arrange

        // Act
        var act = async () => await systemUnderTest.Execute(Description, locationId, SorCode);

        // Assert
        await act.Should().ThrowExactlyAsync<T>();
    }

    [Theory]
    [MemberData(nameof(InvalidArgumentTestData))]
#pragma warning disable xUnit1026
#pragma warning disable CA1707
    public async void GivenAnInvalidSorCode_WhenExecuting_ThenExceptionIsThrown<T>(T exception,
        string sorCode) where T : Exception
#pragma warning restore CA1707
#pragma warning restore xUnit1026
    {
        // Arrange

        // Act
        var act = async () => await systemUnderTest.Execute(Description, LocationId, sorCode);

        // Assert
        await act.Should().ThrowExactlyAsync<T>();
    }

    public static TheoryData<ArgumentException, string> InvalidArgumentTestData() => new()
    {
        { new ArgumentNullException(), null },
        { new ArgumentException(), "" },
        { new ArgumentException(), " " },
    };
}
