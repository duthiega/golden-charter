using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldenCharter.DocumentGenerator.Core.Enums;

namespace GoldenCharter.DocumentGenerator.Core.Interfaces
{
    public interface ITemplateRepository
    {
        Task<string> GetTemplateAsync(DocumentType type, CancellationToken cancellationToken = default);
    }
}
