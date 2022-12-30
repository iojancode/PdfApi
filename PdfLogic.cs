using System;
using System.IO;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace PdfApi
{
    public static class PdfLogic
    {
        public static async Task<Stream> FromHtml(string body)
        {
            using var browserFetcher = new BrowserFetcher();
            await browserFetcher.DownloadAsync();

            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true, Args = new string[] { "--no-sandbox" } });
            await using var page = await browser.NewPageAsync();
            await page.SetContentAsync(body);
            return await page.PdfStreamAsync(new PdfOptions { PrintBackground = true });
        }
    }
}
