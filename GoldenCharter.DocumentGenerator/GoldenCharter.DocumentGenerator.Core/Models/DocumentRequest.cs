using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GoldenCharter.DocumentGenerator.Core.Enums;
using GoldenCharter.DocumentGenerator.Core.Interfaces;

namespace GoldenCharter.DocumentGenerator.Core.Models
{
    public class DocumentRequest
    {
        public required string Type { get; init; }
        public required JsonElement Data { get; init; }
    }
}
