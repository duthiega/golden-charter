using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenCharter.DocumentGenerator.Core.Models.Partials
{
    public record FuneralDetails
    {
        public string? Plan { get; init; } 
        public DateOnly Date { get; init; }
        public string? Time { get; init; }
        public string? Location { get; init; }
        public string? ServiceType { get; init; } 
    }
}
