using System.Text.Json;
using GoldenCharter.DocumentGenerator.Core.Deserialisers;
using GoldenCharter.DocumentGenerator.Core.Enums;
using GoldenCharter.DocumentGenerator.Core.Models.Documents;

namespace GoldenCharter.DocumentGenerator.Tests
{
    public class DocumentDeserialiserTests
    {
        [Fact]
        public void DanDeserialize_ReturnsCorrectModel()
        {
            // Arrange
            var json = """
            {
                "planHolder": {
                    "firstName": "John",
                    "lastName": "Doe"
                },
                "funeralHome": {
                    "name": "Test Home"
                },
                "funeralDetails": {
                    "plan": "Basic"
                },
                "lineItems": []
            }
            """;

            var jsonElement = JsonSerializer.Deserialize<JsonElement>(json);
            var deserialiser = new DanDocumentDeserialiser();

            // Act
            var result = deserialiser.Deserialize(jsonElement);

            // Assert
            Assert.IsType<DanDocumentData>(result);
            var typed = (DanDocumentData)result;
            Assert.Equal("John", typed.PlanHolder.FirstName);
            Assert.Equal("Doe", typed.PlanHolder.LastName);
            Assert.Equal("Test Home", typed.FuneralHome.Name);
        }

        [Fact]
        public void IddDeserialize_ReturnsCorrectModel()
        {
            // Arrange
            var json = """
            {
                "planHolder": {
                    "firstName": "Alice",
                    "lastName": "Smith"
                },
                "funeralHome": {
                    "name": "Peaceful Funerals"
                },
                "funeralDetails": {
                    "plan": "Standard"
                },
                "paymentDetails": {
                    "arrangementFee": 250,
                    "method": "credit-card"
                }
            }
            """;

            var jsonElement = JsonSerializer.Deserialize<JsonElement>(json);
            var deserialiser = new IddDocumentDeserialiser();

            // Act
            var result = deserialiser.Deserialize(jsonElement);

            // Assert
            Assert.IsType<IddDocumentData>(result);
            var typed = (IddDocumentData)result;
            Assert.Equal("Alice", typed.PlanHolder.FirstName);
            Assert.Equal("Smith", typed.PlanHolder.LastName);
            Assert.Equal("Peaceful Funerals", typed.FuneralHome.Name);
            Assert.Equal("credit-card", typed.PaymentDetails.Method);
        }

        [Fact]
        public void Deserialize_Throws_WhenJsonIsInvalid()
        {
            // Arrange
            var invalidJson = """ { "unexpected": "value" } """;
            var jsonElement = JsonSerializer.Deserialize<JsonElement>(invalidJson);
            var deserialiser = new DanDocumentDeserialiser();

            // Act & Assert
            Assert.Throws<JsonException>(() => deserialiser.Deserialize(jsonElement));
        }

        [Fact]
        public void IddDocumentDeserialiser_Type_ReturnsIdd()
        {
            // Arrange
            var deserialiser = new IddDocumentDeserialiser();

            // Act
            var type = deserialiser.Type;

            // Assert
            Assert.Equal(DocumentType.Idd, type);
        }
    }
}
