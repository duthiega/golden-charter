using GoldenCharter.DocumentGenerator.Core.Enums;
using GoldenCharter.DocumentGenerator.Core.Interfaces;
using GoldenCharter.DocumentGenerator.Core.Models;
using HandlebarsDotNet;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;


public class DocumentService : IDocumentService
{
    private readonly ITemplateRepository _templateRepository;
    private readonly IDocumentRequestDeserialisationResolver _resolver;

    static DocumentService()
    {
        // Register global Handlebars helpers
        Handlebars.RegisterHelper("eq", (writer, context, parameters) =>
        {
            if (parameters.Length == 2 && parameters[0]?.ToString() == parameters[1]?.ToString())
            {
                writer.Write(true);
            }
        });
    }
    public DocumentService(
        ITemplateRepository templateRepository,
        IDocumentRequestDeserialisationResolver resolver)
    {
        _templateRepository = templateRepository;
        _resolver = resolver;
    }

    public async Task<byte[]> GenerateAsync(DocumentRequest request, CancellationToken cancellationToken = default)
    {
        if (!Enum.TryParse<DocumentType>(request.Type, true, out var documentType))
        {
            throw new ArgumentException("Unrecognised document type.");
        }

        var deserializer = _resolver.GetDeserialiser(documentType);
        object documentData = deserializer.Deserialize(request.Data);

        var template = await _templateRepository.GetTemplateAsync(documentType, cancellationToken);
        var compiledTemplate = Handlebars.Compile(template);
        var renderedHtml = compiledTemplate(documentData);

        // Convert HTML to PDF using Syncfusion
        var htmlConverter = new HtmlToPdfConverter(HtmlRenderingEngine.Blink);
        var settings = new BlinkConverterSettings
        {
            ViewPortSize = new Syncfusion.Drawing.Size(1280, 0),
            EnableJavaScript = true
        };
        htmlConverter.ConverterSettings = settings;

        using PdfDocument document = htmlConverter.Convert(renderedHtml, string.Empty);
        using MemoryStream stream = new();
        document.Save(stream);
        return stream.ToArray();
    }

}
