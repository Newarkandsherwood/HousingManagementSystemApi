namespace HousingManagementSystemApi.Tests.HelperTests;

using System;
using System.Collections.Generic;
using FluentAssertions;
using Helpers;
using Microsoft.Azure.Cosmos;
using Xunit;

public class ContainerResolverTests
{
    private readonly ContainerResolver systemUnderTest;

    private readonly Dictionary<string, Container> cosmosAddressContainers = new();

    public ContainerResolverTests()
    {
        systemUnderTest = new ContainerResolver(cosmosAddressContainers);
    }

    [Fact]
    public void GivenNullCosmosAddressContainer_WhenConstructing_ThenExceptionIsThrown()
    {
        var act = () => new ContainerResolver(null);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Theory]
    [MemberData(nameof(TestHelpers.RepairTypeTestData.InvalidRepairTypeArgumentTestData), MemberType = typeof(TestHelpers.RepairTypeTestData))]
    public void GivenInvalidRepairTypeParameter_WhenResolving_ThenExceptionIsThrown<T>(T exception,
        string repairTypeParameter) where T : Exception
    {
        // Arrange

        // Act
        Action act = () => _ = systemUnderTest.Resolve(repairTypeParameter);

        // Assert
        act.Should().Throw<T>();
    }

    [Theory]
    [MemberData(nameof(TestHelpers.RepairTypeTestData.ValidRepairTypeArgumentTestData), MemberType = typeof(TestHelpers.RepairTypeTestData))]
    public void GivenValidRepairTypeParameter_WhenResolving_ThenExceptionIsNotThrown(string repairTypeParameter)
    {
        // Act
        Action act = () => _ = systemUnderTest.Resolve(repairTypeParameter);

        // Assert
        act.Should().NotThrow<ArgumentException>();
    }

    [Fact]
    public void GivenContainerForRepairType_WhenResolving_ThenContainerIsReturned()
    {
        // Arrange
        const string repairType = RepairType.Tenant;
        Container tenantContainer = default;
        cosmosAddressContainers[repairType] = tenantContainer;

        // Act
        var actual = systemUnderTest.Resolve(repairType);

        // Assert
        actual.Should().BeSameAs(tenantContainer);
    }

    [Fact]
    public void GivenLackOfContainerForRepairType_WhenResolving_ThenExceptionIsThrown()
    {
        // Arrange
        const string repairType = RepairType.Tenant;

        // Act
        var act = () => systemUnderTest.Resolve(repairType);

        // Assert
        act.Should().Throw<NotSupportedException>().WithMessage($"Cosmos DB container for repair Type '{repairType}' not configured");
    }
}
