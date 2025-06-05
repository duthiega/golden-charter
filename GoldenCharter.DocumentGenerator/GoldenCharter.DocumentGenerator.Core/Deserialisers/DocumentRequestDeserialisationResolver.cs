using GoldenCharter.DocumentGenerator.Core.Enums;
using GoldenCharter.DocumentGenerator.Core.Interfaces;

namespace GoldenCharter.DocumentGenerator.Core.Deserialisers
{
    public class DocumentRequestDeserialisationResolver : IDocumentRequestDeserialisationResolver
    {
        private readonly Dictionary<DocumentType, IDocumentRequestDeserialiser> _deserialisers;

        public DocumentRequestDeserialisationResolver(IEnumerable<IDocumentRequestDeserialiser> deserialisers)
        {
            _deserialisers = deserialisers.ToDictionary(s => s.Type);
        }

        public IDocumentRequestDeserialiser GetDeserialiser(DocumentType type)
        {
            if (_deserialisers.TryGetValue(type, out var deserialiser))
                return deserialiser;

            throw new NotSupportedException($"No deserialiser for type '{type}'");
        }
    }
}
