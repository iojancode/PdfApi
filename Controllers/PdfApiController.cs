using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PdfApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PdfApiController : ControllerBase
    {
        private readonly ILogger<PdfApiController> _logger;

        public PdfApiController(ILogger<PdfApiController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Consumes("text/html")]
        public async Task<IActionResult> FromHtml([FromBody] string body, [FromHeader(Name = "x-encrypt-pass")] string password)
        {
            if (string.IsNullOrWhiteSpace(body)) return BadRequest("Requires html body");

            var stream = await PdfLogic.FromHtml(body, password);
            _logger.LogInformation("fromHtml lenght={lenght}", body.Length);

            return File(stream, "application/pdf");
        }
    }
}
