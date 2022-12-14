using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace PdfApi.AspNetCore
{
    public class TextHtmlInputFormatter : InputFormatter
    {
        private const string ContentType = "text/html";

        public TextHtmlInputFormatter()
        {
            SupportedMediaTypes.Add(ContentType);
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            using (var reader = new StreamReader(context.HttpContext.Request.Body))
            {
                var content = await reader.ReadToEndAsync();
                return await InputFormatterResult.SuccessAsync(content);
            }
        }

        public override bool CanRead(InputFormatterContext context)
        {
            return context.HttpContext.Request.ContentType == ContentType;
        }
    }
}