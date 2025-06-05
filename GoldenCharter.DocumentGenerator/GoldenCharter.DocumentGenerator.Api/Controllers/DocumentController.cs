using System.Net.Mime;
using GoldenCharter.DocumentGenerator.Core.Interfaces;
using GoldenCharter.DocumentGenerator.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoldenCharter.DocumentGenerator.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }


        [HttpPost("generate-link")]
        public async Task<IActionResult> GenerateLink([FromBody] DocumentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var pdfBytes = await _documentService.GenerateAsync(request, cancellationToken);
                var fileName = $"{request.Type}-{Guid.NewGuid():N}.pdf";

                return File(pdfBytes, "application/pdf", fileName);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Log the exception (e.g., using ILogger) if available
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpPost("download")]
        public async Task<IActionResult> Download([FromBody] DocumentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var pdfBytes = await _documentService.GenerateAsync(request, cancellationToken);
                return File(pdfBytes, "application/pdf");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Log the exception (e.g., using ILogger) if available
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

    }
}
