using GoldenCharter.DocumentGenerator.Core.Enums;
using GoldenCharter.DocumentGenerator.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace GoldenCharter.DocumentGenerator.Core.Decorators
{
    public class DocumentRequestDeserialisationResolverExceptionLoggingDecorator : IDocumentRequestDeserialisationResolver
    {
        private readonly IDocumentRequestDeserialisationResolver _inner;
        private readonly ILogger<DocumentRequestDeserialisationResolverExceptionLoggingDecorator> _logger;

        public DocumentRequestDeserialisationResolverExceptionLoggingDecorator(
            IDocumentRequestDeserialisationResolver inner,
            ILogger<DocumentRequestDeserialisationResolverExceptionLoggingDecorator> logger)
        {
            _inner = inner;
            _logger = logger;
        }

        public IDocumentRequestDeserialiser GetDeserialiser(DocumentType type)
        {
            try
            {
                _logger.LogDebug("Resolving deserialiser for type: {DocumentType}", type);
                var deserialiser = _inner.GetDeserialiser(type);
                _logger.LogDebug("Resolved deserialiser for type: {DocumentType}", type);
                return deserialiser;
            }
            catch (NotSupportedException ex)
            {
                _logger.LogWarning(ex, "Unsupported document type requested: {DocumentType}", type);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error resolving deserialiser for type: {DocumentType}", type);
                throw new ApplicationException($"Unexpected error resolving deserialiser for type '{type}'.", ex);
            }
        }
    }
}
