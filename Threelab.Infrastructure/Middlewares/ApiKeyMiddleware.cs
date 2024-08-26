using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Text.Json;
using Threelab.Domain.Abstracts;
using Threelab.Domain.Entities;
using Threelab.Domain.Interfaces;
using Threelab.Domain.Interfaces.Services;
using Threelab.Domain.Models.Error;
using static System.Formats.Asn1.AsnWriter;

namespace Threelab.Infrastructure.Middlewares
{
    public class ApiKeyMiddleware
    {
        private const string APIKEY = "x-api-key";
        private readonly RequestDelegate _next;

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IServiceProvider serviceProvider)
        {
            if (!httpContext.Request.Headers.TryGetValue(APIKEY, out
               var extractedApiKey))
            {
                var errObj = new FailedResult("API key null!!!", (int)HttpStatusCodes.UNAUTHORIZED, true);

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(errObj));
            }
            else
            {
                var apiKey = extractedApiKey;

                using (var scope = serviceProvider.CreateScope())
                {
                    var apiKeyService = scope.ServiceProvider.GetRequiredService<IApiKey>();
                    var apikey = await apiKeyService.GetOne(apiKey);
                    var errObj = new FailedResult("API KEY IS INVALID!!!", (int)HttpStatusCodes.UNAUTHORIZED, true);

                    if (apikey is null)
                    {
                        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(errObj));
                    }
                    else
                    {
                        await _next(httpContext);
                    }
                }
            }
        }
    }
}