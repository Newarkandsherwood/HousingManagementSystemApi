namespace HousingManagementSystemApi.Tests.HelperTests
{
    using HACT.Dtos;
    using Helpers;
    using Microsoft.Azure.Cosmos;
    using Moq;
    using Xunit;

    public class CapitaGatewayHelperTests
    {
        private CapitaGatewayHelper systemUnderTest;
        private string place_ref = "place_ref";
        private string std_job_code = "std_job_code";
        private string client_ref = "client_ref";
        private string source = "source";
        private string sor = "sor";
        private string location = "location";
        private string quantity = "quantity";
        private string description = "description";

        public CapitaGatewayHelperTests()
        {
            systemUnderTest = new CapitaGatewayHelper();
        }

        [Fact]
#pragma warning disable CA1707
        public void GivenValid_place_ref_WhenCreateLogJobRequest_ThenResponseContainsCorrectValue()
#pragma warning restore CA1707
        {
            // Arrange

            // Act
            var result = systemUnderTest.CreateLogJobRequest(place_ref, std_job_code, client_ref, source, sor, location, quantity, description);

            // Assert
            Assert.Equal(place_ref, result.Parameters.Find(p => p.Attribute == "place_ref")?.AttributeValue);
        }

        [Fact]
#pragma warning disable CA1707
        public void GivenValid_std_job_code_WhenCreateLogJobRequest_ThenResponseContainsCorrectValue()
#pragma warning restore CA1707
        {
            // Arrange

            // Act
            var result = systemUnderTest.CreateLogJobRequest(place_ref, std_job_code, client_ref, source, sor, location, quantity, description);

            // Assert
            Assert.Equal(std_job_code, result.Parameters.Find(p => p.Attribute == "std_job_code")?.AttributeValue);
        }

        [Fact]
#pragma warning disable CA1707
        public void GivenValid_client_ref_WhenCreateLogJobRequest_ThenResponseContainsCorrectValue()
#pragma warning restore CA1707
        {
            // Arrange

            // Act
            var result = systemUnderTest.CreateLogJobRequest(place_ref, std_job_code, client_ref, source, sor, location, quantity, description);

            // Assert
            Assert.Equal(client_ref, result.Parameters.Find(p => p.Attribute == "client_ref")?.AttributeValue);
        }

        [Fact]
#pragma warning disable CA1707
        public void GivenValid_source_WhenCreateLogJobRequest_ThenResponseContainsCorrectValue()
#pragma warning restore CA1707
        {
            // Arrange

            // Act
            var result = systemUnderTest.CreateLogJobRequest(place_ref, std_job_code, client_ref, source, sor, location, quantity, description);

            // Assert
            Assert.Equal(source, result.Parameters.Find(p => p.Attribute == "source")?.AttributeValue);
        }

        [Fact]
#pragma warning disable CA1707
        public void GivenValid_sor_WhenCreateLogJobRequest_ThenResponseContainsCorrectValue()
#pragma warning restore CA1707
        {
            // Arrange

            // Act
            var result = systemUnderTest.CreateLogJobRequest(place_ref, std_job_code, client_ref, source, sor, location, quantity, description);

            // Assert
            Assert.Equal(sor, result.Lines.Line.Parameters.Find(p => p.Attribute == "sor")?.AttributeValue);
        }

        [Fact]
#pragma warning disable CA1707
        public void GivenValid_location_WhenCreateLogJobRequest_ThenResponseContainsCorrectValue()
#pragma warning restore CA1707
        {
            // Arrange

            // Act
            var result = systemUnderTest.CreateLogJobRequest(place_ref, std_job_code, client_ref, source, sor, location, quantity, description);

            // Assert
            Assert.Equal(location, result.Lines.Line.Parameters.Find(p => p.Attribute == "location")?.AttributeValue);
        }

        [Fact]
#pragma warning disable CA1707
        public void GivenValid_quantity_WhenCreateLogJobRequest_ThenResponseContainsCorrectValue()
#pragma warning restore CA1707
        {
            // Arrange

            // Act
            var result = systemUnderTest.CreateLogJobRequest(place_ref, std_job_code, client_ref, source, sor, location, quantity, description);

            // Assert
            Assert.Equal(quantity, result.Lines.Line.Parameters.Find(p => p.Attribute == "quantity")?.AttributeValue);
        }

        [Fact]
#pragma warning disable CA1707
        public void GivenValid_description_WhenCreateLogJobRequest_ThenResponseContainsCorrectValue()
#pragma warning restore CA1707
        {
            // Arrange

            // Act
            var result = systemUnderTest.CreateLogJobRequest(place_ref, std_job_code, client_ref, source, sor, location, quantity, description);

            // Assert
            Assert.Equal(this.description, result.Lines.Line.Parameters.Find(p => p.Attribute == "description")?.AttributeValue);
        }
    }
}
