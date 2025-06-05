using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldenCharter.DocumentGenerator.Core.Models;

namespace GoldenCharter.DocumentGenerator.Core.Interfaces
{
    public interface IDocumentService
    {
        Task<byte[]> GenerateAsync(DocumentRequest request, CancellationToken cancellationToken = default);
    }
}
