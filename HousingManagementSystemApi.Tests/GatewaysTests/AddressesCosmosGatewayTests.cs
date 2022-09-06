namespace HousingManagementSystemApi.Tests.GatewaysTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Gateways;
    using HACT.Dtos;
    using Moq;
    using Repositories;
    using Xunit;

    public class AddressesCosmosGatewayTests
    {
        private AddressesCosmosGateway systemUnderTest;
        private Mock<IAddressesRepository> addressesRepositoryMock;

        public AddressesCosmosGatewayTests()
        {
            this.addressesRepositoryMock = new Mock<IAddressesRepository>();
            this.systemUnderTest = new AddressesCosmosGateway();
        }


        [Fact]
        public async void Test_Get_addresses()
        {
            // Arrange

            // Act
            Func<Task> act = async () => await this.systemUnderTest.SearchByPostcode("NG21 9LQ");

            // Assert
            await act.Should().NotThrowAsync();
        }

//         [Fact]
// #pragma warning disable CA1707
//         public async void GivenValidPostcodeArgument_WhenSearchingForPostcode_ThenAddressesAreRetrievedFromTheDatabase()
// #pragma warning restore CA1707
//         {
//             // Arrange
//             const string postcode = "M3 OW";
//             addressesRepositoryMock.Setup(repository => repository.GetAddressesByPostcode(postcode))
//                 .ReturnsAsync(new[] { new PropertyAddress() });
//
//             // Act
//             var results = await this.systemUnderTest.SearchByPostcode(postcode);
//
//             // Assert
//             Assert.True(results.Any());
//         }
    }
}
