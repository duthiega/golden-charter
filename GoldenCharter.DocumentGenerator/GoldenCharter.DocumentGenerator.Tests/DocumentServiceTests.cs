using System.Text.Json;
using GoldenCharter.DocumentGenerator.Core.Decorators;
using GoldenCharter.DocumentGenerator.Core.Enums;
using GoldenCharter.DocumentGenerator.Core.Interfaces;
using GoldenCharter.DocumentGenerator.Core.Models;
using Microsoft.Extensions.Logging;
using Moq;

namespace GoldenCharter.DocumentGenerator.Tests
{
    public class DocumentServiceTests
    {
        private readonly Mock<ITemplateRepository> _mockTemplateRepo;
        private readonly Mock<IDocumentRequestDeserialisationResolver> _mockResolver;
        private readonly Mock<IDocumentRequestDeserialiser> _mockDeserializer;
        private readonly Mock<ILogger<DocumentServiceExceptionLoggingDecorator>> _mockLogger;
        private readonly DocumentRequest _validRequest;
        private readonly string _validTemplate;
        private readonly Mock<IDocumentData> _mockData;

        public DocumentServiceTests()
        {
            _mockTemplateRepo = new Mock<ITemplateRepository>();
            _mockResolver = new Mock<IDocumentRequestDeserialisationResolver>();
            _mockDeserializer = new Mock<IDocumentRequestDeserialiser>();
            _mockLogger = new Mock<ILogger<DocumentServiceExceptionLoggingDecorator>>();
            _mockData = new Mock<IDocumentData>();

            _validRequest = new DocumentRequest
            {
                Type = DocumentType.Dan.ToString(),
                Data = JsonSerializer.SerializeToElement(new { Name = "Test User" })
            };

            _validTemplate = "<html><body><p>{{Name}}</p></body></html>";

            _mockTemplateRepo
                .Setup(repo => repo.GetTemplateAsync(DocumentType.Dan, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_validTemplate);

            _mockResolver
                .Setup(r => r.GetDeserialiser(DocumentType.Dan))
                .Returns(_mockDeserializer.Object);

            _mockDeserializer
                .Setup(d => d.Deserialize(It.IsAny<JsonElement>()))
                .Returns(_mockData.Object);
        }

        [Fact]
        public async Task GenerateAsync_ReturnsPdfBytes_WhenValidRequestProvided()
        {
            var sut = new DocumentService(_mockTemplateRepo.Object, _mockResolver.Object);

            var result = await sut.GenerateAsync(_validRequest);

            Assert.NotNull(result);
            Assert.True(result.Length > 0);
        }

        [Fact]
        public async Task GenerateAsync_ThrowsArgumentException_ForInvalidType()
        {
            var invalidRequest = new DocumentRequest
            {
                Type = "unknown",
                Data = JsonSerializer.SerializeToElement(new { })
            };

            var sut = new DocumentService(_mockTemplateRepo.Object, _mockResolver.Object);

            await Assert.ThrowsAsync<ArgumentException>(() => sut.GenerateAsync(invalidRequest));
        }

        [Fact]
        public async Task GenerateAsync_ThrowsApplicationException_OnUnhandledError()
        {
            var mockInnerService = new Mock<IDocumentService>();
            mockInnerService
                .Setup(s => s.GenerateAsync(It.IsAny<DocumentRequest>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("Something failed"));

            var decorator = new DocumentServiceExceptionLoggingDecorator(mockInnerService.Object, _mockLogger.Object);

            var failingRequest = new DocumentRequest
            {
                Type = DocumentType.Dan.ToString(),
                Data = JsonSerializer.SerializeToElement(new { Name = "Failing" })
            };

            var ex = await Assert.ThrowsAsync<ApplicationException>(() => decorator.GenerateAsync(failingRequest));
            Assert.Equal("An unexpected error occurred while generating the document.", ex.Message);
        }
    }
}
