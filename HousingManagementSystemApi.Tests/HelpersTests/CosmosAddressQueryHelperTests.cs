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
        private const string MockPostcode = "NG21 9LQ";

        public CosmosAddressQueryHelperTests()
        {
            containerMock = new Mock<Container>();
            systemUnderTest = new CosmosAddressQueryHelper(containerMock.Object);
            feedIteratorMock = new Mock<FeedIterator<PropertyAddress>>();
        }

        [Fact]
#pragma warning disable CA1707
        public void Test_Query_Definition_returned_with_querytext()
#pragma warning restore CA1707
        {
            // Arrange
            feedIteratorMock.Setup(_ => _.HasMoreResults).Returns(false);
            containerMock
                .Setup(_ => _.GetItemQueryIterator<PropertyAddress>("TEST", null, null)) //It.IsAny<string>()
                .Returns(feedIteratorMock.Object);

            // Act
            var result = systemUnderTest.GetItemQueryIterator<PropertyAddress>(MockPostcode);

            // Assert
            containerMock.Verify(m => m.GetItemQueryIterator<PropertyAddress>(
                It.Is<QueryDefinition>(u =>
                        u.QueryText ==
                        "SELECT * FROM c WHERE (UPPER(REPLACE(c.PostalCode, ' ','')))  = (UPPER(REPLACE(@postcode, ' ','')))  ORDER BY c.AddressLine[0] ASC"
                )
                , It.IsAny<string>(), null));
        }

        [Fact]
#pragma warning disable CA1707
        public void Test_Query_Definition_returned_with_parameter()
#pragma warning restore CA1707
        {
            // Arrange
            feedIteratorMock.Setup(_ => _.HasMoreResults).Returns(false);
            containerMock
                .Setup(_ => _.GetItemQueryIterator<PropertyAddress>("TEST", null, null))
                .Returns(feedIteratorMock.Object);

            // Act
            var result = systemUnderTest.GetItemQueryIterator<PropertyAddress>(MockPostcode);

            // Assert
            containerMock.Verify(m => m.GetItemQueryIterator<PropertyAddress>(
                It.Is<QueryDefinition>(u =>
                    u.GetQueryParameters()[0].Name == "@postcode"
                    && u.GetQueryParameters()[0].Value.ToString() == MockPostcode
                ),
                It.IsAny<string>(), null));
        }
    }
}
