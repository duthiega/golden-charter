using GoldenCharter.DocumentGenerator.Core.Decorators;
using GoldenCharter.DocumentGenerator.Core.Enums;
using GoldenCharter.DocumentGenerator.Core.Interfaces;
using GoldenCharter.DocumentGenerator.Core.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System.Text.Json;

namespace GoldenCharter.DocumentGenerator.Tests
{
    public class DecoratorTests
    {
        [Fact]
        public async Task DocumentServiceDecorator_LogsInformation_OnSuccess()
        {
            var mockLogger = new Mock<ILogger<DocumentServiceExceptionLoggingDecorator>>();
            var mockInner = new Mock<IDocumentService>();

            var request = new DocumentRequest
            {
                Type = DocumentType.Dan.ToString(),
                Data = JsonSerializer.SerializeToElement(new { })
            };

            mockInner.Setup(x => x.GenerateAsync(request, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new byte[] { 0x1 });

            var sut = new DocumentServiceExceptionLoggingDecorator(mockInner.Object, mockLogger.Object);

            var result = await sut.GenerateAsync(request);

            Assert.NotNull(result);
            mockLogger.Verify(
                l => l.Log(LogLevel.Information, It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Starting PDF generation")),
                null, It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once);
        }

        [Fact]
        public async Task DocumentServiceDecorator_LogsAndRethrowsArgumentException()
        {
            var mockLogger = new Mock<ILogger<DocumentServiceExceptionLoggingDecorator>>();
            var mockInner = new Mock<IDocumentService>();

            var request = new DocumentRequest
            {
                Type = "InvalidType",
                Data = JsonSerializer.SerializeToElement(new { })
            };

            mockInner.Setup(x => x.GenerateAsync(request, It.IsAny<CancellationToken>()))
                     .ThrowsAsync(new ArgumentException("Invalid"));

            var sut = new DocumentServiceExceptionLoggingDecorator(mockInner.Object, mockLogger.Object);

            await Assert.ThrowsAsync<ArgumentException>(() => sut.GenerateAsync(request));
            mockLogger.Verify(l => l.Log(LogLevel.Warning, It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(), It.IsAny<ArgumentException>(), It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once);
        }

        [Fact]
        public async Task TemplateRepositoryDecorator_LogsWarning_WhenFileNotFound()
        {
            var mockLogger = new Mock<ILogger<TemplateRepositoryExceptionLoggingDecorator>>();
            var mockInner = new Mock<ITemplateRepository>();

            mockInner.Setup(r => r.GetTemplateAsync(DocumentType.Idd, It.IsAny<CancellationToken>()))
                     .ThrowsAsync(new FileNotFoundException("Missing"));

            var sut = new TemplateRepositoryExceptionLoggingDecorator(mockInner.Object, mockLogger.Object);

            await Assert.ThrowsAsync<FileNotFoundException>(() =>
                sut.GetTemplateAsync(DocumentType.Idd));

            mockLogger.Verify(l => l.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<FileNotFoundException>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once);
        }

        [Fact]
        public void GetDeserialiser_DelegatesToInner()
        {
            // Arrange
            var mockInner = new Mock<IDocumentRequestDeserialisationResolver>();
            var mockLogger = new Mock<ILogger<DocumentRequestDeserialisationResolverExceptionLoggingDecorator>>();

            var expected = Mock.Of<IDocumentRequestDeserialiser>();
            mockInner.Setup(r => r.GetDeserialiser(DocumentType.Dan)).Returns(expected);

            var sut = new DocumentRequestDeserialisationResolverExceptionLoggingDecorator(mockInner.Object, mockLogger.Object);

            // Act
            var result = sut.GetDeserialiser(DocumentType.Dan);

            // Assert
            Assert.Same(expected, result);
        }
    }
}

