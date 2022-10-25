using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using HACT.Dtos;
using HousingManagementSystemApi.Gateways;
using HousingManagementSystemApi.UseCases;
using Moq;
using Xunit;
namespace HousingManagementSystemApi.Tests
{

    public class RetrieveAddressesUseCaseTests
    {
        private readonly Mock<IAddressesGateway> retrieveAddressesGateway;
        private readonly RetrieveAddressesUseCase retrieveAddressesUseCase;

        public RetrieveAddressesUseCaseTests()
        {
            retrieveAddressesGateway = new Mock<IAddressesGateway>();
            retrieveAddressesUseCase = new RetrieveAddressesUseCase(retrieveAddressesGateway.Object);
        }

        [Fact]
        public async Task GivenAPostcodeAndRepairType_WhenExecute_GatewayReceivesCorrectInput()
        {
            const string TestPostcode = "postcode";
            const string TestRepairType = "TENANT";
            retrieveAddressesGateway.Setup(x => x.SearchByPostcode(TestPostcode, TestRepairType));
            await retrieveAddressesUseCase.Execute(TestPostcode, TestRepairType);
            retrieveAddressesGateway.Verify(x => x.SearchByPostcode(TestPostcode, TestRepairType), Times.Once);
        }

        [Fact]
        public async Task GivenAPostcodeAndRepairType_WhenAnAddressExists_GatewayReturnsCorrectData()
        {
            const string TestPostcode = "postcode";
            const string TestRepairType = "TENANT";
            retrieveAddressesGateway.Setup(x => x.SearchByPostcode(TestPostcode, TestRepairType))
                .ReturnsAsync(new PropertyAddress[] { new() { PostalCode = TestPostcode } });
            var result = await retrieveAddressesUseCase.Execute(TestPostcode, TestRepairType);
            result.First().PostalCode.Should().Be(TestPostcode);
        }

        [Fact]
        public async void GivenNullPostcode_WhenExecute_ThrowsNullException()
        {
            Func<Task> act = async () => await retrieveAddressesUseCase.Execute(null, null);
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async void GivenEmptyPostcode_WhenExecute_SearchByPostcodeIsNotCalled()
        {
            const string TestPostcode = "";
            const string TestRepairType = "TENANT";
            await retrieveAddressesUseCase.Execute(postcode: TestPostcode, repairType: TestRepairType);
            retrieveAddressesGateway.Verify(x => x.SearchByPostcode(TestPostcode), Times.Never);
        }

        [Fact]
        public async void GivenNullRepairType_WhenExecute_ThrowsNullException()
        {
            const string TestPostcode = "postcode";
            Func<Task> act = async () => await retrieveAddressesUseCase.Execute(TestPostcode, null);
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async void GivenEmptyRepairType_WhenExecute_SearchByPostcodeIsNotCalled()
        {
            const string TestPostcode = "postcode";
            Func<Task> act = async () => await retrieveAddressesUseCase.Execute(TestPostcode, "");
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("Repair type must be a valid value");
        }
    }

}
