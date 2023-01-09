namespace HousingManagementSystemApi.Tests.UseCasesTests;

using System;
using FluentAssertions;
using Gateways;
using Xunit;

public class DummyWorkOrderGatewayTests
{
    private const string LocationId = "locationId";
    private const string SorCode = "SOR_CODE";

    private readonly DummyWorkOrderGateway systemUnderTest = new();

    [Theory]
    [MemberData(nameof(InvalidArgumentTestData))]
#pragma warning disable xUnit1026
#pragma warning disable CA1707
    public async void GivenAnInvalidLocationId_WhenCreatingWorkOrder_ThenExceptionIsThrown<T>(T exception,
        string locationId) where T : Exception
#pragma warning restore CA1707
#pragma warning restore xUnit1026
    {
        // Arrange

        // Act
        var act = async () => await systemUnderTest.CreateWorkOrder(locationId, SorCode);

        // Assert
        await act.Should().ThrowExactlyAsync<T>();
    }


    [Theory]
    [MemberData(nameof(InvalidArgumentTestData))]
#pragma warning disable xUnit1026
#pragma warning disable CA1707
    public async void GivenAnInvalidSorCode_WhenCreatingWorkOrder_ThenExceptionIsThrown<T>(T exception,
        string sorCode) where T : Exception
#pragma warning restore CA1707
#pragma warning restore xUnit1026
    {
        // Arrange

        // Act
        var act = async () => await systemUnderTest.CreateWorkOrder(LocationId, sorCode);

        // Assert
        await act.Should().ThrowExactlyAsync<T>();
    }

    [Fact]
    public async void GivenValidArguments_WhenCreatingWorkOrder_ThenValidWorkOrderIdIsReturned()
    {
        // Arrange

        // Act
        var workOrderId = await systemUnderTest.CreateWorkOrder(LocationId, SorCode);

        // Assert
        workOrderId.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async void GivenValidArguments_WhenCreatingMultipleWorkOrders_ThenUniqueWorkOrderIdsAreReturned()
    {
        // Arrange

        // Act
        var workOrderId = await systemUnderTest.CreateWorkOrder(LocationId, SorCode);
        var workOrderId2 = await systemUnderTest.CreateWorkOrder(LocationId, SorCode);

        // Assert
        workOrderId.Should().NotBe(workOrderId2);
    }

    public static TheoryData<ArgumentException, string> InvalidArgumentTestData() => new()
    {
        { new ArgumentNullException(), null },
        { new ArgumentException(), "" },
        { new ArgumentException(), " " },
    };
}
