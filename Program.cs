using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using PuppeteerSharp;

namespace PdfApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Contains("--initBrowser")) InitBrowser();
            else CreateHostBuilder(args).Build().Run();
        }

        public static void InitBrowser() 
        {
            using var browserFetcher = new BrowserFetcher();
            browserFetcher.DownloadAsync().GetAwaiter().GetResult();
        } 

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
