using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GoldenCharter.DocumentGenerator.Core.Enums;

namespace GoldenCharter.DocumentGenerator.Core.Interfaces
{
    public interface IDocumentRequestDeserialiser
    {
        DocumentType Type { get; }
        IDocumentData Deserialize(JsonElement element);
    }
}
