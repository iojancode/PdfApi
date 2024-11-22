using System;
using System.IO;
using System.Threading.Tasks;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace PdfApi
{
    static class PdfLogic
    {
        public static Task<Stream> FromHtml(string body, string password = null)
        {
            return FromHtml(new PdfOptions { PrintBackground = true }, body, password);
        }

        public static Task<Stream> FromHtml(HtmlModel model, string password = null)
        {
            var options = new PdfOptions { PrintBackground = true };
            
            if (model.HeaderTemplate != null) options.HeaderTemplate = model.HeaderTemplate;
            if (model.FooterTemplate != null) options.FooterTemplate = model.FooterTemplate;
            if (model.FooterTemplate != null || model.HeaderTemplate != null) options.DisplayHeaderFooter = true;

            if (model.MarginTop != null || model.MarginBottom != null || model.MarginLeft != null || model.MarginRight != null)
                options.MarginOptions = new MarginOptions() { 
                    Top = model.MarginTop, Bottom = model.MarginBottom, Left = model.MarginLeft, Right = model.MarginRight };

            return FromHtml(options, model.Html, password);          
        }

        private static async Task<Stream> FromHtml(PdfOptions options, string body, string password)
        {
            using var browserFetcher = new BrowserFetcher();
            await browserFetcher.DownloadAsync();

            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true, Args = new string[] { 
                "--no-sandbox", "--disable-setuid-sandbox", "--font-render-hinting=none", "--force-color-profile=srgb" } });
            await using var page = await browser.NewPageAsync();
            await page.SetContentAsync(body);
            var result = await page.PdfStreamAsync(options);

            if (string.IsNullOrWhiteSpace(password)) return result;
            else return EncryptLogic.EncryptPDFWithQPDF(result, password);            
        }
    }
}
