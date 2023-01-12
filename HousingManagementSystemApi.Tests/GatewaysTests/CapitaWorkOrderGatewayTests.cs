namespace HousingManagementSystemApi.Tests.GatewaysTests
{
    using System;
    using FluentAssertions;
    using Gateways;
    using Helpers;
    using Models.Capita;
    using Moq;
    using Services;
    using Xunit;

    public class CapitaWorkOrderGatewayTests
    {
        private readonly CapitaWorkOrderGateway systemUnderTest;
        private const string LocationId = "locationId";
        private const string SorCode = "SOR_CODE";
        private const string Description = "SOR_CODE";
        private Mock<ICapitaService> capitaService;

        public CapitaWorkOrderGatewayTests()
        {
            this.capitaService = new Mock<ICapitaService>();
            systemUnderTest = new CapitaWorkOrderGateway(capitaService.Object);
        }

        [Theory]
        [MemberData(nameof(InvalidArgumentTestData))]
#pragma warning disable xUnit1026
#pragma warning disable CA1707
        public async void GivenAnInvalidDescription_WhenCreatingWorkOrder_ThenExceptionIsThrown<T>(T exception,
            string description) where T : Exception
#pragma warning restore CA1707
#pragma warning restore xUnit1026
        {
            // Arrange

            // Act
            var act = async () => await systemUnderTest.CreateWorkOrder(description, LocationId, SorCode);

            // Assert
            await act.Should().ThrowExactlyAsync<T>();
        }

        [Theory]
        [MemberData(nameof(InvalidArgumentTestData))]
#pragma warning disable xUnit1026
#pragma warning disable CA1707
        public async void GivenAnInvalidLocationId_WhenCreatingWorkOrder_ThenExceptionIsThrown<T>(T exception,
            string locationId) where T : Exception
#pragma warning restore CA1707
#pragma warning restore xUnit1026
        {
            // Arrange

            // Act
            var act = async () => await systemUnderTest.CreateWorkOrder(Description, locationId, SorCode);

            // Assert
            await act.Should().ThrowExactlyAsync<T>();
        }

        [Theory]
        [MemberData(nameof(InvalidArgumentTestData))]
#pragma warning disable xUnit1026
#pragma warning disable CA1707
        public async void GivenAnInvalidSorCode_WhenCreatingWorkOrder_ThenExceptionIsThrown<T>(T exception,
            string sorCode) where T : Exception
#pragma warning restore CA1707
#pragma warning restore xUnit1026
        {
            // Arrange

            // Act
            var act = async () => await systemUnderTest.CreateWorkOrder(Description, LocationId, sorCode);

            // Assert
            await act.Should().ThrowExactlyAsync<T>();
        }

        public static TheoryData<ArgumentException, string> InvalidArgumentTestData() => new()
        {
            { new ArgumentNullException(), null },
            { new ArgumentException(), "" },
            { new ArgumentException(), " " },
        };
        [Fact]
        public async void GivenValidParameters_WhenCreateWorkOrder_ThenCapitaServiceLogJobIsCalled()
        {
            // Arrange
            capitaService.Setup(service => service.LogJob(It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<string>()))
                .ReturnsAsync("WorkOrderID");

            // Act
            await systemUnderTest.CreateWorkOrder(Description, LocationId, SorCode);

            // Assert
            capitaService.VerifyAll();
        }
    }
}
