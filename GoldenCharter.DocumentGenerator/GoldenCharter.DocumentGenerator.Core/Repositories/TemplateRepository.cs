using GoldenCharter.DocumentGenerator.Core.Enums;
using GoldenCharter.DocumentGenerator.Core.Interfaces;
using GoldenCharter.DocumentGenerator.Core.Options;
using Microsoft.Extensions.Options;

namespace GoldenCharter.DocumentGenerator.Core.Repositories
{
    public class TemplateRepository : ITemplateRepository
    {
        private readonly string _templatePath;

        public TemplateRepository(IOptions<TemplateOptions> options)
        {
            _templatePath = options?.Value?.TemplatePath
                ?? throw new ArgumentNullException(nameof(options), "Template options must be provided.");
        }

        public async Task<string> GetTemplateAsync(DocumentType type, CancellationToken cancellationToken = default)
        {
            var filePath = Path.Combine(_templatePath, $"{type.ToString().ToLowerInvariant()}.html");

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Template file not found for type: {type}", filePath);

            return await File.ReadAllTextAsync(filePath, cancellationToken);
        }
    }
}
