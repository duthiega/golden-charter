using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GoldenCharter.DocumentGenerator.Core.Enums;
using GoldenCharter.DocumentGenerator.Core.Interfaces;
using GoldenCharter.DocumentGenerator.Core.Models.Documents;

namespace GoldenCharter.DocumentGenerator.Core.Deserialisers
{
    public class IddDocumentDeserialiser : DocumentDeserialiserBase<IddDocumentData>
    {
        public override DocumentType Type => DocumentType.Idd;
    }
}
