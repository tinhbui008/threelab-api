using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;
using Threelab.Domain.Abstracts;
using Threelab.Domain.Entities;
using Threelab.Domain.Interfaces;
using Threelab.Domain.Interfaces.Services;
using Threelab.Domain.Models.Error;

namespace Threelab.Infrastructure.Middlewares
{
    public class ApiKeyMiddleware
    {
        private const string APIKEY = "x-api-key";
        private readonly RequestDelegate _next;
        private readonly IApiKey _apiKey;

        public ApiKeyMiddleware(RequestDelegate next, IApiKey apiKey)
        {
            _next = next;
            _apiKey = apiKey;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (!httpContext.Request.Headers.TryGetValue(APIKEY, out
               var extractedApiKey))
            {
                var errObj = new FailedResult("API key null!!!", (int)HttpStatusCodes.UNAUTHORIZED);
                await HandleExceptionAsync(httpContext, errObj);
            }

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