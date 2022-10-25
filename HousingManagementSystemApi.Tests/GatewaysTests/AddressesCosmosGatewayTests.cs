namespace HousingManagementSystemApi.Tests.GatewaysTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Gateways;
    using HACT.Dtos;
    using Helpers;
    using Microsoft.Azure.Cosmos;
    using Moq;
    using Xunit;

    public class AddressesCosmosGatewayTests
    {
        private readonly AddressesCosmosGateway systemUnderTest;

        private readonly Mock<IContainerResolver> containerResolverMock;
        private readonly Mock<FeedIterator<PropertyAddress>> feedIteratorMock;
        private readonly Mock<ICosmosAddressQueryHelper> cosmosQueryHelperMock;
        private readonly Mock<Container> tenantContainerMock;
        private readonly Mock<FeedResponse<PropertyAddress>> feedResponseMock;
        private readonly Mock<Container> communalContainerMock;

        private const string MockPostcode = "NG21 9LQ";
        private const string MockTenantRepairType = "TENANT";
        private const string MockCommunalRepairType = "COMMUNAL";
        private const string TenantAddress = "ABC Tenant House";
        private const string CommunalAddress = "Block ABC: Communal House";

        public AddressesCosmosGatewayTests()
        {
            tenantContainerMock = new Mock<Container>();
            communalContainerMock = new Mock<Container>();
            containerResolverMock = new Mock<IContainerResolver>();
            systemUnderTest = new AddressesCosmosGateway(containerResolverMock.Object);
            cosmosQueryHelperMock = new Mock<ICosmosAddressQueryHelper>();
            feedIteratorMock = new Mock<FeedIterator<PropertyAddress>>();
            feedResponseMock = new Mock<FeedResponse<PropertyAddress>>();
        }

        [Fact]
#pragma warning disable CA1707
        public async void Test_Does_Not_Throws_Exception_When_Iterator_Returns_No_Results()
#pragma warning restore CA1707
        {
            // Arrange
            feedIteratorMock.Setup(_ => _.HasMoreResults).Returns(false);
            containerResolverMock.Setup(x => x.Resolve(It.IsAny<string>())).Returns(tenantContainerMock.Object);
            cosmosQueryHelperMock.Setup(_ => _.GetItemQueryIterator<PropertyAddress>(MockPostcode))
                .Returns(feedIteratorMock.Object);
            tenantContainerMock
                .Setup(c => c.GetItemQueryIterator<PropertyAddress>(
                    It.IsAny<QueryDefinition>(),
                    It.IsAny<string>(),
                    It.IsAny<QueryRequestOptions>()))
                .Returns(feedIteratorMock.Object);

            // Act
            Func<Task> act = async () => await systemUnderTest.SearchByPostcode(MockPostcode, MockTenantRepairType);

            // Assert
            await act.Should().NotThrowAsync();
        }

        [Fact]
#pragma warning disable CA1707
        public async void Test_No_Addresses_Are_Returned_When_Iterator_Returns_No_Results()
#pragma warning restore CA1707
        {
            // Arrange
            feedIteratorMock.Setup(_ => _.HasMoreResults).Returns(false);
            containerResolverMock.Setup(x => x.Resolve(It.IsAny<string>())).Returns(tenantContainerMock.Object);
            cosmosQueryHelperMock.Setup(_ => _.GetItemQueryIterator<PropertyAddress>(MockPostcode))
                .Returns(feedIteratorMock.Object);
            tenantContainerMock
                .Setup(c => c.GetItemQueryIterator<PropertyAddress>(
                    It.IsAny<QueryDefinition>(),
                    It.IsAny<string>(),
                    It.IsAny<QueryRequestOptions>()))
                .Returns(feedIteratorMock.Object);

            // Act
            var results = await systemUnderTest.SearchByPostcode(MockPostcode, MockTenantRepairType);

            // Assert
            Assert.False(results.Any());
        }

        [Fact]
#pragma warning disable CA1707
        public async void Test_Tenant_Addresses_Are_Returned_When_Repair_Type_Is_Tenant()
#pragma warning restore CA1707
        {
            // Arrange
            feedIteratorMock.Setup(_ => _.HasMoreResults).Returns(true);
            var tenantAddressList = new List<PropertyAddress> { new() { BuildingName = TenantAddress, CityName = "test", PostalCode = MockPostcode } };

            feedResponseMock.Setup(x => x.GetEnumerator()).Returns(tenantAddressList.GetEnumerator());

            containerResolverMock.Setup(x => x.Resolve(MockTenantRepairType)).Returns(tenantContainerMock.Object);

            feedIteratorMock
                .Setup(f => f.ReadNextAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(feedResponseMock.Object)
                .Callback(() => feedIteratorMock
                    .Setup(f => f.HasMoreResults)
                    .Returns(false));

            cosmosQueryHelperMock.Setup(x => x.GetItemQueryIterator<PropertyAddress>(MockPostcode))
                .Returns(feedIteratorMock.Object);

            tenantContainerMock
                .Setup(c => c.GetItemQueryIterator<PropertyAddress>(
                    It.IsAny<QueryDefinition>(),
                    It.IsAny<string>(),
                    It.IsAny<QueryRequestOptions>()))
                .Returns(feedIteratorMock.Object);

            // Act
            var results = await systemUnderTest.SearchByPostcode(MockPostcode, MockTenantRepairType);

            // Assert
            Assert.Equal(TenantAddress, results.FirstOrDefault().BuildingName);
        }

        [Fact]
#pragma warning disable CA1707
        public async void Test_Communal_Addresses_Are_Returned_When_Repair_Type_Is_Communal()
#pragma warning restore CA1707
        {
            // Arrange
            feedIteratorMock.Setup(_ => _.HasMoreResults).Returns(true);
            var communalAddressList = new List<PropertyAddress> { new() { BuildingName = CommunalAddress, CityName = "test", PostalCode = MockPostcode } };

            feedResponseMock.Setup(x => x.GetEnumerator()).Returns(communalAddressList.GetEnumerator());

            containerResolverMock.Setup(x => x.Resolve(MockCommunalRepairType)).Returns(communalContainerMock.Object);

            feedIteratorMock
                .Setup(f => f.ReadNextAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(feedResponseMock.Object)
                .Callback(() => feedIteratorMock
                    .Setup(f => f.HasMoreResults)
                    .Returns(false));

            cosmosQueryHelperMock.Setup(x => x.GetItemQueryIterator<PropertyAddress>(MockPostcode))
                .Returns(feedIteratorMock.Object);

            communalContainerMock
                .Setup(c => c.GetItemQueryIterator<PropertyAddress>(
                    It.IsAny<QueryDefinition>(),
                    It.IsAny<string>(),
                    It.IsAny<QueryRequestOptions>()))
                .Returns(feedIteratorMock.Object);

            // Act
            var results = await systemUnderTest.SearchByPostcode(MockPostcode, MockCommunalRepairType);

            // Assert
            Assert.Equal(CommunalAddress, results.FirstOrDefault().BuildingName);
        }
    }
}
