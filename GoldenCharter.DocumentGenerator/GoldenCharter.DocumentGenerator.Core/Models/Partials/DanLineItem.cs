using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenCharter.DocumentGenerator.Core.Models.Partials
{
    public record DanLineItem
    {
        public string? Description { get; set; } 
        public int Quantity { get; init; }
        public decimal Price { get; init; }
        public decimal Total { get; init; }
    }
}
