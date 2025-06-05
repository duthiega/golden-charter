using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldenCharter.DocumentGenerator.Core.Enums;
using GoldenCharter.DocumentGenerator.Core.Models.Partials;

namespace GoldenCharter.DocumentGenerator.Core.Interfaces
{
    public interface IDocumentData
    {
        DocumentType Type { get; init; }

        PlanHolder PlanHolder { get; init; }
    }
}

