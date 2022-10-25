namespace HousingManagementSystemApi.Tests.HelperTests;

using System;
using Helpers;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;
using ServiceCollectionExtensions = Helpers.ServiceCollectionExtensions;

public class ServiceCollectionExtensionsTests
{
    [Fact (Skip = "Need to figure out how to stub the return value of the GetCosmosClientContainer method or similar as it's trying to create a CosmosDB Client during the test")]
    public void GivenValidRepairTypes_WhenAddingCosmosAddressContainersToServices_ThenContainerResolverIsRegistered()
    {
        var serviceCollectionMock = new Mock<IServiceCollection>();
        serviceCollectionMock.Setup(x =>
            x.Add(It.Is<ServiceDescriptor>(serviceDescriptor =>
                    serviceDescriptor.ServiceType == typeof(IContainerResolver) &&
                    serviceDescriptor.ImplementationFactory == null
                )
            )
        );
        var serviceCollection = serviceCollectionMock.Object;

        const string testRepairType = "repairType";
        var containerMock = new Mock<Container>();
        var cosmosClientMock = new Mock<CosmosClient>();

        cosmosClientMock.Setup(c => c.GetContainer(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(containerMock.Object);

        // Act
        ServiceCollectionExtensions.AddCosmosAddressContainers(serviceCollection, new []{testRepairType} );

        // Assert
        serviceCollectionMock.VerifyAll();
    }
}
