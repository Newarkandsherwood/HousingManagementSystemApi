namespace HousingManagementSystemApi.Tests.HelperTests
{
    using HACT.Dtos;
    using Helpers;
    using Microsoft.Azure.Cosmos;
    using Moq;
    using Xunit;

    public class CosmosAddressQueryHelperTests
    {
        private CosmosAddressQueryHelper systemUnderTest;
        private Mock<Container> containerMock;
        private Mock<FeedIterator<PropertyAddress>> feedIteratorMock;

        public CosmosAddressQueryHelperTests()
        {
            this.containerMock = new Mock<Container>();
            this.systemUnderTest = new CosmosAddressQueryHelper(this.containerMock.Object);
            this.feedIteratorMock = new Mock<FeedIterator<PropertyAddress>>();
        }

        [Fact]
        public void Test_Query_Definition_returned_with_querytext()
        {
            // Arrange
            this.feedIteratorMock.Setup(_ => _.HasMoreResults).Returns(false);
            this.containerMock
                .Setup(_ => _.GetItemQueryIterator<PropertyAddress>("TEST", null, null)) //It.IsAny<string>()
                .Returns(this.feedIteratorMock.Object);

            // Act
            var result = this.systemUnderTest.GetItemQueryIterator<PropertyAddress>("NG21 7HT");

            // Assert
            this.containerMock.Verify(m => m.GetItemQueryIterator<PropertyAddress>(
                It.Is<QueryDefinition>(u =>
                        u.QueryText ==
                        "SELECT * FROM c WHERE (UPPER(REPLACE(c.PostalCode, ' ','')))  = (UPPER(REPLACE(@postcode, ' ','')))  ORDER BY c.AddressLine[0] ASC"
                )
                , It.IsAny<string>(), null));
        }

        [Fact]
        public void Test_Query_Definition_returned_with_parameter()
        {
            // Arrange
            this.feedIteratorMock.Setup(_ => _.HasMoreResults).Returns(false);
            this.containerMock
                .Setup(_ => _.GetItemQueryIterator<PropertyAddress>("TEST", null, null))
                .Returns(this.feedIteratorMock.Object);

            // Act
            var result = this.systemUnderTest.GetItemQueryIterator<PropertyAddress>("NG21 7HT");

            // Assert
            this.containerMock.Verify(m => m.GetItemQueryIterator<PropertyAddress>(
                It.Is<QueryDefinition>(u =>
                    u.GetQueryParameters()[0].Name == "@postcode"
                    && u.GetQueryParameters()[0].Value.ToString() == "NG21 7HT"
                ),
                It.IsAny<string>(), null));
        }
    }
}
