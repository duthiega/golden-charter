using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldenCharter.DocumentGenerator.Core.Enums;
using GoldenCharter.DocumentGenerator.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace GoldenCharter.DocumentGenerator.Core.Decorators
{
    public class TemplateRepositoryExceptionLoggingDecorator : ITemplateRepository
    {
        private readonly ITemplateRepository _inner;
        private readonly ILogger<TemplateRepositoryExceptionLoggingDecorator> _logger;

        public TemplateRepositoryExceptionLoggingDecorator(
            ITemplateRepository inner,
            ILogger<TemplateRepositoryExceptionLoggingDecorator> logger)
        {
            _inner = inner;
            _logger = logger;
        }

        public async Task<string> GetTemplateAsync(DocumentType type, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _inner.GetTemplateAsync(type, cancellationToken);
            }
            catch (FileNotFoundException ex)
            {
                _logger.LogWarning(ex, "Template not found for type: {DocumentType}", type);
                throw;
            }
            catch (IOException ex)
            {
                _logger.LogError(ex, "I/O error reading template for type: {DocumentType}", type);
                throw new ApplicationException($"I/O error reading template for type: {type}", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error retrieving template for type: {DocumentType}", type);
                throw new ApplicationException($"Unexpected error retrieving template for type: {type}", ex);
            }
        }
    }

}


