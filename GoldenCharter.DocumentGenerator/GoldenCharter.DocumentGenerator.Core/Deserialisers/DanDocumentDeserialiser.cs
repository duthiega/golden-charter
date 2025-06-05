using System.Text.Json;
using GoldenCharter.DocumentGenerator.Core.Enums;
using GoldenCharter.DocumentGenerator.Core.Interfaces;
using GoldenCharter.DocumentGenerator.Core.Models.Documents;

namespace GoldenCharter.DocumentGenerator.Core.Deserialisers
{
    public class DanDocumentDeserialiser : DocumentDeserialiserBase<DanDocumentData>
    {
        public override DocumentType Type => DocumentType.Dan;
    }
}
