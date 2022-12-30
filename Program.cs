using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

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
            PdfLogic.FromHtml("<body>init</body>").GetAwaiter().GetResult();
        } 

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
