using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PdfApi.AspNetCore
{
    // taken from https://www.c-sharpcorner.com/article/using-api-key-authentication-to-secure-asp-net-core-web-api/
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        
        private const string APIKEY_HEADER = "x-api-key";
        private static readonly string API_KEY = Environment.GetEnvironmentVariable("API_KEY");

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(APIKEY_HEADER, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api Key was not provided");
                return;
            }
            if (API_KEY != extractedApiKey)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized client");
                return;
            }
            await _next(context);
        }
    }
}