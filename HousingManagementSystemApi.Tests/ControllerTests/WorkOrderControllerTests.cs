using System;
using FluentAssertions;
using HousingManagementSystemApi.Controllers;
using HousingManagementSystemApi.Gateways;
using HousingManagementSystemApi.UseCases;
using Moq;
using Xunit;

namespace HousingManagementSystemApi.Tests.ControllersTests
{
    public class WorkOrderControllerTests : ControllerTests
    {
        private readonly WorkOrderController systemUnderTest;
        private Mock<ICreateWorkOrderUseCase> createWorkOrderUseCaseMock;
        private Mock<IWorkOrderGateway> workOrderGatewayMock;
        private const string LocationId = "locationId";
        private const string SorCode = "SOR_CODE";
        private const string WorkOrderId = "test";
        private const string Description = "description";

        public WorkOrderControllerTests()
        {
            workOrderGatewayMock = new Mock<IWorkOrderGateway>();
            createWorkOrderUseCaseMock = new Mock<ICreateWorkOrderUseCase>();
            systemUnderTest = new WorkOrderController(createWorkOrderUseCaseMock.Object);
        }

        [Fact]
        public async void GivenValidParameters_WhenCreateWorkOrder_ThenWorkOrderUseCaseExecuteIsCalled()
        {
            // Arrange
            createWorkOrderUseCaseMock.Setup(useCase => useCase.Execute(It.IsAny<string>(), It.IsAny<string>(),It.IsAny<string>()))
                .ReturnsAsync(WorkOrderId);

            // Act
            await systemUnderTest.CreateWorkOrder(Description, LocationId, SorCode);

            // Assert
            createWorkOrderUseCaseMock.VerifyAll();
        }

        [Fact]
        public async void GivenValidParameters_WhenCreateWorkOrder_ThenWorkOrderUseCaseReturnsStatus200()
        {
            // Arrange
            createWorkOrderUseCaseMock.Setup(useCase => useCase.Execute(It.IsAny<string>(),It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(WorkOrderId);

            // Act
            var result = await systemUnderTest.CreateWorkOrder(Description, LocationId, SorCode);

            // Assert
            GetStatusCode(result).Should().Be(200);
        }

        [Fact]
        public async void GivenValidParameters_WhenCreateWorkOrder_ThenWorkOrderUseCaseIsCalledWithCorrectParameters()
        {
            // Arrange
            createWorkOrderUseCaseMock.Setup(useCase => useCase.Execute(It.IsAny<string>(),It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(WorkOrderId);

            // Act
            await systemUnderTest.CreateWorkOrder(Description, LocationId, SorCode);

            // Assert
            createWorkOrderUseCaseMock.Verify(x => x.Execute(Description, LocationId, SorCode), Times.Once);
        }

        [Fact]
        public async void GivenExceptionReturnedByWorkOrderUseCase_WhenCreateWorkOrder_ThenErrorReturned()
        {
            // Arrange
            var exceptionMessage = "Exception Message";
            createWorkOrderUseCaseMock.Setup(useCase => useCase.Execute(It.IsAny<string>(),It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new SystemException(exceptionMessage));

            // Act
            var result = await systemUnderTest.CreateWorkOrder(Description, LocationId, SorCode);

            // Assert
            GetStatusCode(result).Should().Be(500);
            GetResultData<string>(result).Should().Be(exceptionMessage);
        }
    }

}
