using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using GoldenCharter.DocumentGenerator.Core.Enums;
using GoldenCharter.DocumentGenerator.Core.Interfaces;
using GoldenCharter.DocumentGenerator.Core.Models.Partials;

namespace GoldenCharter.DocumentGenerator.Core.Models.Documents
{
    public class DanDocumentData : IDocumentData
    {
        public required PlanHolder PlanHolder { get; init; }
        public required FuneralHome FuneralHome { get; init; }
        public required FuneralDetails FuneralDetails { get; init; }
        public List<DanLineItem> LineItems { get; set; } = new();

        [JsonIgnore]
        public decimal SubTotal => LineItems.Select(x => x.Total).Sum();

        [JsonIgnore]
        public decimal InstalmentCharge { get; set; } = 0.00m;

        [JsonIgnore]
        public decimal TotalAmount => SubTotal + InstalmentCharge;

        public DocumentType Type { get; init; }
    }
}
