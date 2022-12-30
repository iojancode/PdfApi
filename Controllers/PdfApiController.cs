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
        public async Task<IActionResult> FromHtml([FromBody] string body)
        {
            var stream = await PdfLogic.FromHtml(body);
            _logger.LogInformation("fromHtml {hash}", body.GetHashCode());

            return File(stream, "application/pdf");
        }
    }
}
