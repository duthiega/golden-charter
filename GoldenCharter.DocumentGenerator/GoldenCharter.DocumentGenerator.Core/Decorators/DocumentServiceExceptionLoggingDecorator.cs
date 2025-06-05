using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using GoldenCharter.DocumentGenerator.Core.Interfaces;
using GoldenCharter.DocumentGenerator.Core.Models;
using Microsoft.Extensions.Logging;

namespace GoldenCharter.DocumentGenerator.Core.Decorators
{

    public class DocumentServiceExceptionLoggingDecorator : IDocumentService
    {
        private readonly IDocumentService _inner;
        private readonly ILogger<DocumentServiceExceptionLoggingDecorator> _logger;

        public DocumentServiceExceptionLoggingDecorator(
            IDocumentService inner,
            ILogger<DocumentServiceExceptionLoggingDecorator> logger)
        {
            _inner = inner;
            _logger = logger;
        }

        public async Task<byte[]> GenerateAsync(DocumentRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Starting PDF generation for type: {DocumentType}", request.Type);
                var result = await _inner.GenerateAsync(request, cancellationToken);
                _logger.LogInformation("PDF generation completed for type: {DocumentType}", request.Type);
                return result;
            }
            catch (ArgumentException argEx)
            {
                _logger.LogWarning(argEx, "Invalid document request: {Type}", request.Type);
                throw; // still allow the caller to handle this
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during PDF generation for type: {Type}", request.Type);
                throw new ApplicationException("An unexpected error occurred while generating the document.", ex);
            }
        }
    }
}
