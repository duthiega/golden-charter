using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenCharter.DocumentGenerator.Core.Deserialisers
{
    using System.Text.Json;
    using GoldenCharter.DocumentGenerator.Core.Enums;
    using GoldenCharter.DocumentGenerator.Core.Interfaces;
    using GoldenCharter.DocumentGenerator.Core.Models;

    public abstract class DocumentDeserialiserBase<T> : IDocumentRequestDeserialiser where T : IDocumentData
    {
        public abstract DocumentType Type { get; }

        private readonly JsonSerializerOptions _serialiserOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public IDocumentData Deserialize(JsonElement element)
        {
            return element.Deserialize<T>(_serialiserOptions)!;
        }
    }
}
