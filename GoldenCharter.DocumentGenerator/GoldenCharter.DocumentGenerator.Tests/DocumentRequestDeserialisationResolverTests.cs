using GoldenCharter.DocumentGenerator.Core.Deserialisers;
using GoldenCharter.DocumentGenerator.Core.Enums;
using GoldenCharter.DocumentGenerator.Core.Interfaces;
using Xunit;

namespace GoldenCharter.DocumentGenerator.Tests
{
    public class DocumentRequestDeserialisationResolverTests
    {
        [Fact]
        public void GetDeserialiser_ReturnsCorrectImplementation()
        {
            // Arrange
            var danDeserialiser = new DanDocumentDeserialiser();
            var resolver = new DocumentRequestDeserialisationResolver(new[] { danDeserialiser });

            // Act
            var result = resolver.GetDeserialiser(DocumentType.Dan);

            // Assert
            Assert.Same(danDeserialiser, result);
        }

        [Fact]
        public void GetDeserialiser_Throws_WhenTypeNotRegistered()
        {
            // Arrange
            var resolver = new DocumentRequestDeserialisationResolver(new IDocumentRequestDeserialiser[] { });

            // Act & Assert
            Assert.Throws<NotSupportedException>(() => resolver.GetDeserialiser(DocumentType.Idd));
        }
    }
}
