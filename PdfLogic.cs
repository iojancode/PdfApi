using System;
using System.IO;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace PdfApi
{
    static class PdfLogic
    {
        public static async Task<Stream> FromHtml(string body, string password = null)
        {
            using var browserFetcher = new BrowserFetcher();
            await browserFetcher.DownloadAsync();

            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true, Args = new string[] { 
                "--no-sandbox", "--disable-setuid-sandbox", "--font-render-hinting=none", "--force-color-profile=srgb" } });
            await using var page = await browser.NewPageAsync();
            await page.SetContentAsync(body);
            var result = await page.PdfStreamAsync(new PdfOptions { PrintBackground = true });

            if (string.IsNullOrWhiteSpace(password)) return result;
            else return EncryptLogic.EncryptPDFWithQPDF(result, password);
        }
    }
}
