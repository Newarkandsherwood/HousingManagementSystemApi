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

        private Mock<ICosmosAddressQueryHelper> cosmosQueryHelperMock;
        private Mock<FeedIterator<PropertyAddress>> feedIteratorMock;

        private const string MockPostcode = "NG21 9LQ";

        public AddressesCosmosGatewayCosmosQueryHelperTests()
        {
            this.cosmosQueryHelperMock = new Mock<ICosmosAddressQueryHelper>();
            this.systemUnderTest = new AddressesCosmosGateway(this.cosmosQueryHelperMock.Object);
            this.feedIteratorMock = new Mock<FeedIterator<PropertyAddress>>();
        }

        [Fact]
        public async void Test_Get_addresses_Not_Throws_Exception()
        {
            // Arrange
            this.feedIteratorMock.Setup(_ => _.HasMoreResults).Returns(false);
            this.cosmosQueryHelperMock.Setup(_ => _.GetItemQueryIterator<PropertyAddress>(MockPostcode))
                .Returns(this.feedIteratorMock.Object);

            // Act
            Func<Task> act = async () => await this.systemUnderTest.SearchByPostcode(MockPostcode);

            // Assert
            await act.Should().NotThrowAsync();
        }

        [Fact]
        public async void Test_No_Addresses_Returned_When_Iterator_No_Results()
        {
            // Arrange
            this.feedIteratorMock.Setup(_ => _.HasMoreResults).Returns(false);
            this.cosmosQueryHelperMock.Setup(_ => _.GetItemQueryIterator<PropertyAddress>(MockPostcode))
                .Returns(this.feedIteratorMock.Object);

            // Act
            var results = await this.systemUnderTest.SearchByPostcode(MockPostcode);

            // Assert
            Assert.False(results.Any());
        }

        [Fact]
        public async void Test_Addresses_Returned_When_Iterator_Results()
        {
            // Arrange
            this.feedIteratorMock.Setup(_ => _.HasMoreResults).Returns(true);
            var addressList = new List<PropertyAddress>();
            addressList.Add(new PropertyAddress() { CityName = "test", PostalCode = MockPostcode });

            var feedResponseMock = new Mock<FeedResponse<PropertyAddress>>();
            feedResponseMock.Setup(x => x.GetEnumerator()).Returns(addressList.GetEnumerator());

            this.feedIteratorMock
                .Setup(f => f.ReadNextAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(feedResponseMock.Object)
                .Callback(() => this.feedIteratorMock
                    .Setup(f => f.HasMoreResults)
                    .Returns(false));

            var containerMock = new Mock<Container>();
            containerMock
                .Setup(c => c.GetItemQueryIterator<PropertyAddress>(
                    It.IsAny<QueryDefinition>(),
                    It.IsAny<string>(),
                    It.IsAny<QueryRequestOptions>()))
                .Returns(this.feedIteratorMock.Object);

            this.cosmosQueryHelperMock.Setup(_ => _.GetItemQueryIterator<PropertyAddress>(MockPostcode))
                .Returns(this.feedIteratorMock.Object);

            // Act
            var results = await this.systemUnderTest.SearchByPostcode(MockPostcode);

            // Assert
            Assert.True(results.Count() == 1);
        }
    }
}
