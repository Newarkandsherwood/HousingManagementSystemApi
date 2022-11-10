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

namespace HousingManagementSystemApi.Tests.ControllersTests
{
    using Helpers;

    public class AddressControllerTests : ControllerTests
    {
        private readonly AddressesController systemUnderTest;
        private readonly Mock<IRetrieveAddressesUseCase> retrieveAddressesUseCaseMock;
        private readonly string postcode;

        public AddressControllerTests()
        {
            postcode = "postcode";
            retrieveAddressesUseCaseMock = new Mock<IRetrieveAddressesUseCase>();
            systemUnderTest = new AddressesController(retrieveAddressesUseCaseMock.Object);
        }

        private void SetupDummyAddresses(string repairType)
        {
            var dummyList = new List<PropertyAddress> { new() { PostalCode = this.postcode } };
            retrieveAddressesUseCaseMock
                .Setup(x => x.Execute(this.postcode, repairType))
                .ReturnsAsync(dummyList);
        }

        private void SetupTenantDummyAddresses()
        {
            SetupDummyAddresses(RepairType.Tenant);
        }

        private void SetupCommunalDummyAddresses()
        {
            SetupDummyAddresses(RepairType.Communal);
        }

        private void SetupLeaseholdDummyAddresses()
        {
            SetupDummyAddresses(RepairType.Leasehold);
        }

        [Fact]
        public async Task GivenAPostcode_WhenAValidRequestForTenantAddressesIsMade_ItReturnsASuccessfulResponse()
        {
            SetupTenantDummyAddresses();

            var result = await systemUnderTest.TenantAddresses(this.postcode);
            retrieveAddressesUseCaseMock.Verify(x => x.Execute(this.postcode, RepairType.Tenant), Times.Once);
            GetStatusCode(result).Should().Be(200);
        }

        [Fact]
        public async Task GivenAPostcode_WhenAValidRequestForTenantAddressesIsMade_ItReturnsCorrectData()
        {
            SetupTenantDummyAddresses();

            var result = await systemUnderTest.TenantAddresses(postcode);
            GetResultData<List<PropertyAddress>>(result).First().PostalCode.Should().Be(this.postcode);
        }

        [Fact]
        public async Task GivenAnExceptionIsThrown_WhenRequestIsMadeForTenantAddresses_ResponseHttpStatusCodeIs500()
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
        public async Task GivenAnExceptionIsThrown_WhenRequestIsMadeForTenantAddresses_ResponseHttpStatusMessageIsExceptionMessage()
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

        [Fact]
        public async Task GivenAPostcode_WhenAValidRequestForCommunalAddressesIsMade_ItReturnsASuccessfulResponse()
        {
            SetupCommunalDummyAddresses();

            var result = await systemUnderTest.CommunalAddresses(this.postcode);
            retrieveAddressesUseCaseMock.Verify(x => x.Execute(this.postcode, RepairType.Communal), Times.Once);
            GetStatusCode(result).Should().Be(200);
        }

        [Fact]
        public async Task GivenAPostcode_WhenAValidRequestForCommunalAddressesIsMade_ItReturnsCorrectData()
        {
            SetupCommunalDummyAddresses();

            var result = await systemUnderTest.CommunalAddresses(postcode);
            GetResultData<List<PropertyAddress>>(result).First().PostalCode.Should().Be(this.postcode);
        }

        [Fact]
        public async Task GivenAnExceptionIsThrown_WhenRequestIsMadeForCommunalAddresses_ResponseHttpStatusCodeIs500()
        {
            // Arrange
            retrieveAddressesUseCaseMock.Setup(x => x.Execute(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<Exception>();

            // Act
            var result = await systemUnderTest.CommunalAddresses(postcode);
            // Assert
            GetStatusCode(result).Should().Be(500);
        }

        [Fact]
        public async Task GivenAnExceptionIsThrown_WhenRequestIsMadeForCommunalAddresses_ResponseHttpStatusMessageIsExceptionMessage()
        {
            // Arrange
            const string errorMessage = "An error message";
            retrieveAddressesUseCaseMock.Setup(x => x.Execute(It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception(errorMessage));

            // Act
            var result = await systemUnderTest.CommunalAddresses(postcode);
            // Assert
            GetResultData<string>(result).Should().Be(errorMessage);
        }

        [Fact]
        public async Task GivenAPostcode_WhenAValidRequestForLeaseholdAddressesIsMade_ItReturnsASuccessfulResponse()
        {
            SetupLeaseholdDummyAddresses();

            var result = await systemUnderTest.LeaseholdAddresses(this.postcode);
            retrieveAddressesUseCaseMock.Verify(x => x.Execute(this.postcode, RepairType.Leasehold), Times.Once);
            GetStatusCode(result).Should().Be(200);
        }

        [Fact]
        public async Task GivenAPostcode_WhenAValidRequestForLeaseholdAddressesIsMade_ItReturnsCorrectData()
        {
            SetupLeaseholdDummyAddresses();

            var result = await systemUnderTest.LeaseholdAddresses(postcode);
            GetResultData<List<PropertyAddress>>(result).First().PostalCode.Should().Be(this.postcode);
        }

        [Fact]
        public async Task GivenAnExceptionIsThrown_WhenRequestIsMadeForLeaseholdAddresses_ResponseHttpStatusCodeIs500()
        {
            // Arrange
            retrieveAddressesUseCaseMock.Setup(x => x.Execute(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<Exception>();

            // Act
            var result = await systemUnderTest.LeaseholdAddresses(postcode);
            // Assert
            GetStatusCode(result).Should().Be(500);
        }

        [Fact]
        public async Task GivenAnExceptionIsThrown_WhenRequestIsMadeForLeaseholdAddresses_ResponseHttpStatusMessageIsExceptionMessage()
        {
            // Arrange
            const string errorMessage = "An error message";
            retrieveAddressesUseCaseMock.Setup(x => x.Execute(It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception(errorMessage));

            // Act
            var result = await systemUnderTest.LeaseholdAddresses(postcode);
            // Assert
            GetResultData<string>(result).Should().Be(errorMessage);
        }

    }

}
