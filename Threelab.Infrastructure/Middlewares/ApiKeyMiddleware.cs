using Azure;
using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;
using Threelab.Domain.Abstracts;
using Threelab.Domain.Interfaces.Services;
using Threelab.Domain.Models.Error;

namespace Threelab.Infrastructure.Middlewares
{
    public class ApiKeyMiddleware
    {
        private const string APIKEY = "x-api-key";
        private readonly RequestDelegate _next;
        private readonly IApiKey _apiKeyService;

        public ApiKeyMiddleware(RequestDelegate next, IApiKey apiKeyService)
        {
            _next = next;
            _apiKeyService = apiKeyService;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (!httpContext.Request.Headers.TryGetValue(APIKEY, out
               var extractedApiKey))
            {
                var errObj = new FailedResult("API key null!!!", (int)HttpStatusCodes.UNAUTHORIZED);
                await HandleExceptionAsync(httpContext, errObj);
            }

            //var apiKey = _apiKeyService.GetOne(extractedApiKey);

            await _next(httpContext);
        }

        private static Task HandleExceptionAsync(HttpContext context, FailedResult failedResult)
        {
            var result = JsonSerializer.Serialize(failedResult);
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(result);
        }
    }
}