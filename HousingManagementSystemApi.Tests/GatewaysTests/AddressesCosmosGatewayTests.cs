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

    public class AddressesCosmosGatewayCosmosQueryHelperTests
    {
        private AddressesCosmosGateway systemUnderTest;

        private Mock<IContainerResolver> containerResolverMock;
        private Mock<FeedIterator<PropertyAddress>> feedIteratorMock;
        private readonly Mock<ICosmosAddressQueryHelper> cosmosQueryHelperMock;
        private readonly Mock<Container> containerMock;

        private const string MockPostcode = "NG21 9LQ";
        private const string MockRepairType = "TENANT";

        public AddressesCosmosGatewayCosmosQueryHelperTests()
        {
            containerMock = new Mock<Container>();
            containerResolverMock = new Mock<IContainerResolver>();
            systemUnderTest = new AddressesCosmosGateway(containerResolverMock.Object);
            cosmosQueryHelperMock = new Mock<ICosmosAddressQueryHelper>();
            feedIteratorMock = new Mock<FeedIterator<PropertyAddress>>();
        }

        [Fact]
#pragma warning disable CA1707
        public async void Test_Get_addresses_Not_Throws_Exception()
#pragma warning restore CA1707
        {
            // Arrange
            feedIteratorMock.Setup(_ => _.HasMoreResults).Returns(false);
            containerResolverMock.Setup(x => x.Resolve(It.IsAny<string>())).Returns(containerMock.Object);
            cosmosQueryHelperMock.Setup(_ => _.GetItemQueryIterator<PropertyAddress>(MockPostcode))
                .Returns(feedIteratorMock.Object);
            containerMock
                .Setup(c => c.GetItemQueryIterator<PropertyAddress>(
                    It.IsAny<QueryDefinition>(),
                    It.IsAny<string>(),
                    It.IsAny<QueryRequestOptions>()))
                .Returns(feedIteratorMock.Object);

            // Act
            Func<Task> act = async () => await systemUnderTest.SearchByPostcode(MockPostcode, MockRepairType);

            // Assert
            await act.Should().NotThrowAsync();
        }

        [Fact]
#pragma warning disable CA1707
        public async void Test_No_Addresses_Returned_When_Iterator_No_Results()
#pragma warning restore CA1707
        {
            // Arrange
            feedIteratorMock.Setup(_ => _.HasMoreResults).Returns(false);
            containerResolverMock.Setup(x => x.Resolve(It.IsAny<string>())).Returns(containerMock.Object);
            cosmosQueryHelperMock.Setup(_ => _.GetItemQueryIterator<PropertyAddress>(MockPostcode))
                .Returns(feedIteratorMock.Object);
            containerMock
                .Setup(c => c.GetItemQueryIterator<PropertyAddress>(
                    It.IsAny<QueryDefinition>(),
                    It.IsAny<string>(),
                    It.IsAny<QueryRequestOptions>()))
                .Returns(feedIteratorMock.Object);

            // Act
            var results = await systemUnderTest.SearchByPostcode(MockPostcode, MockRepairType);

            // Assert
            Assert.False(results.Any());
        }

        [Fact]
#pragma warning disable CA1707
        public async void Test_Addresses_Returned_When_Iterator_Results()
#pragma warning restore CA1707
        {
            // Arrange
            feedIteratorMock.Setup(_ => _.HasMoreResults).Returns(true);
            var addressList = new List<PropertyAddress> { new() { CityName = "test", PostalCode = MockPostcode } };
            var feedResponseMock = new Mock<FeedResponse<PropertyAddress>>();
            feedResponseMock.Setup(x => x.GetEnumerator()).Returns(addressList.GetEnumerator());

            containerResolverMock.Setup(x => x.Resolve(It.IsAny<string>())).Returns(containerMock.Object);

            feedIteratorMock
                .Setup(f => f.ReadNextAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(feedResponseMock.Object)
                .Callback(() => feedIteratorMock
                    .Setup(f => f.HasMoreResults)
                    .Returns(false));

            cosmosQueryHelperMock.Setup(x => x.GetItemQueryIterator<PropertyAddress>(MockPostcode))
                .Returns(feedIteratorMock.Object);

            // var containerMock = new Mock<Container>();
            containerMock
                .Setup(c => c.GetItemQueryIterator<PropertyAddress>(
                    It.IsAny<QueryDefinition>(),
                    It.IsAny<string>(),
                    It.IsAny<QueryRequestOptions>()))
                .Returns(feedIteratorMock.Object);

            // Act
            var results = await systemUnderTest.SearchByPostcode(MockPostcode, MockRepairType);

            // Assert
            Assert.Single(results);
        }
    }
}
