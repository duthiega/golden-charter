using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldenCharter.DocumentGenerator.Core.Enums;
using GoldenCharter.DocumentGenerator.Core.Interfaces;
using GoldenCharter.DocumentGenerator.Core.Models.Partials;

namespace GoldenCharter.DocumentGenerator.Core.Models.Documents
{
    public class IddDocumentData : IDocumentData
    {
        public required PlanHolder PlanHolder { get; init; }
        public required FuneralHome FuneralHome { get; init; }
        public required FuneralDetails FuneralDetails { get; init; }
        public PaymentDetails PaymentDetails { get; set; } = new();
        public DocumentType Type { get; init; }
    }
}
