using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using GoldenCharter.DocumentGenerator.Core.Convertors;

namespace GoldenCharter.DocumentGenerator.Core.Models.Partials
{
    public record FuneralHome
    {
        public string? Name { get; init; }

        [JsonConverter(typeof(StringOrNumberConverter))]
        public string? BranchNumber { get; init; }
        public Address? Address { get; init; }
        public SubmittedBy? SubmittedBy { get; init; }
    }
}
