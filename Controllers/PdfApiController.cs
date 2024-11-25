using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PdfApi.Controllers
{
    [ApiController]
    public class PdfApiController : ControllerBase
    {
        private readonly ILogger<PdfApiController> _logger;

        public PdfApiController(ILogger<PdfApiController> logger)
        {
            _logger = logger;
        }

        [Consumes("text/html")]
        [HttpPost("[controller]/fromHtml")]
        public async Task<IActionResult> FromHtml([FromBody] string body, [FromHeader(Name = "x-encrypt-pass")] string password)
        {
            if (string.IsNullOrWhiteSpace(body)) return BadRequest("Requires html body");

            var stream = await PdfLogic.FromHtml(body, password);
            _logger.LogInformation("fromHtml lenght={lenght}", body.Length);

            return File(stream, "application/pdf");
        }

        [Consumes("multipart/form-data")]
        [HttpPost("[controller]/fromHtml")]
        public async Task<IActionResult> MultipartFromHtml([FromForm] HtmlModel model, [FromHeader(Name = "x-encrypt-pass")] string password)
        {
            if (string.IsNullOrWhiteSpace(model.Html)) return BadRequest("Requires html field");

            var stream = await PdfLogic.FromHtml(model, password);
            _logger.LogInformation("fromHtml multipart lenght={lenght}", model.Html.Length);

            return File(stream, "application/pdf");
        }
    }
}
