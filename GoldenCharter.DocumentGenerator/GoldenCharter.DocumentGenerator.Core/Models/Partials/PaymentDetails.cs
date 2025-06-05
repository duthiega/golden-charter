using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenCharter.DocumentGenerator.Core.Models.Partials
{
    public record PaymentDetails
    {
        public decimal ArrangementFee { get; init; }
        public string? Method { get; init; }
        public List<string>? AllowedMethods { get; init; }
    }
}
