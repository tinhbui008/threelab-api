using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Threelab.Application.Core.Abstractions;
using Threelab.Domain.Entities;
using Threelab.Domain.Interfaces.Services;
using Threelab.Service.Services;

namespace Threelab.Application.Core.ActionFilter
{
    public class MyAuthAsync : Attribute, IAsyncActionFilter
    {
        private const string apiKey = "x-api-key";
        private const string token = "x-token";

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var headers = context.HttpContext.Request.Headers.ToList();
            var xToken = headers.Where(c => c.Key == token).Select(c => c.Value).FirstOrDefault();
            var xAPIKey = headers.Where(c => c.Key == apiKey).Select(c => c.Value).FirstOrDefault();

            var user = context.HttpContext.User;

            var keyTokenService = context.HttpContext.RequestServices.GetRequiredService<IKeyToken>();

            return next();
        }
    }
}
