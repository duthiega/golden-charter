using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenCharter.DocumentGenerator.Core.Models.Partials
{
    public record Address
    {
        public string? Street { get; init; }
        public string? City { get; init; }
        public string? County { get; init; }
        public string? Postcode { get; init; }
        public string? Country { get; init; }
    }
}
