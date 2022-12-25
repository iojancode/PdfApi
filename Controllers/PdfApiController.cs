using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PuppeteerSharp;

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
            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true, Args = new string[] { "--no-sandbox" } });
            await using var page = await browser.NewPageAsync();
            await page.SetContentAsync(body);
            var stream = await page.PdfStreamAsync(new PdfOptions { PrintBackground = true });

            _logger.LogInformation("fromHtml {hash}", body.GetHashCode());
            return File(stream, "application/pdf");
        }
    }
}
