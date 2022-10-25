using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using HACT.Dtos;
using HousingManagementSystemApi.Controllers;
using HousingManagementSystemApi.UseCases;
using Moq;
using Xunit;

namespace HousingManagementSystemApi.Tests.ContollersTests
{
    public class AddressControllerTests : ControllerTests
    {
        private readonly AddressesController systemUnderTest;
        private readonly Mock<IRetrieveAddressesUseCase> retrieveAddressesUseCaseMock;
        private readonly string postcode;
        private readonly string repairType;

        public AddressControllerTests()
        {
            postcode = "postcode";
            repairType = "TENANT";

            retrieveAddressesUseCaseMock = new Mock<IRetrieveAddressesUseCase>();
            systemUnderTest = new AddressesController(retrieveAddressesUseCaseMock.Object);
        }

        private void SetupDummyAddresses()
        {
            var dummyList = new List<PropertyAddress> { new() { PostalCode = this.postcode } };
            retrieveAddressesUseCaseMock
                .Setup(x => x.Execute(this.postcode, this.repairType))
                .ReturnsAsync(dummyList);
        }

        [Fact]
        public async Task GivenAPostcode_WhenAValidAddressRequestIsMade_ItReturnsASuccessfulResponse()
        {
            SetupDummyAddresses();

            var result = await systemUnderTest.TenantAddresses(this.postcode);
            retrieveAddressesUseCaseMock.Verify(x => x.Execute(this.postcode, this.repairType), Times.Once);
            GetStatusCode(result).Should().Be(200);
        }

        [Fact]
        public async Task GivenAPostcode_WhenAValidAddressRequestIsMade_ItReturnsCorrectData()
        {
            SetupDummyAddresses();

            var result = await systemUnderTest.TenantAddresses(postcode);
            GetResultData<List<PropertyAddress>>(result).First().PostalCode.Should().Be(this.postcode);
        }

        [Fact]
        public async Task GivenAnExceptionIsThrown_WhenRequestMadeForAddresses_ResponseHttpStatusCodeIs500()
        {
            // Arrange
            retrieveAddressesUseCaseMock.Setup(x => x.Execute(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<Exception>();

            // Act
            var result = await systemUnderTest.TenantAddresses(postcode);
            // Assert
            GetStatusCode(result).Should().Be(500);
        }

        [Fact]
        public async Task GivenAnExceptionIsThrown_WhenRequestMadeForAddresses_ResponseHttpStatusMessageIsExceptionMessage()
        {
            // Arrange
            const string errorMessage = "An error message";
            retrieveAddressesUseCaseMock.Setup(x => x.Execute(It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception(errorMessage));

            // Act
            var result = await systemUnderTest.TenantAddresses(postcode);
            // Assert
            GetResultData<string>(result).Should().Be(errorMessage);
        }

    }

}
