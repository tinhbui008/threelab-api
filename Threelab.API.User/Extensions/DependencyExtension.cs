using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Threelab.Domain.Entities;
using Threelab.Domain.Interfaces;
using Threelab.Domain.Interfaces.Services;
using Threelab.Domain.Models;
using Threelab.Infrastructure;
using Threelab.Infrastructure.Context;
using Threelab.Infrastructure.Middlewares;
using Threelab.Service.Services;

namespace Threelab.API.User.Extensions
{
    public static class DependencyExtension
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddDbContext<DBContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("AccountConnection"),
                    sqlOptions => sqlOptions.CommandTimeout(120));
                options.UseLazyLoadingProxies();
            });

            services.AddScoped<Func<DBContext>>((provider) => () => provider.GetService<DBContext>());
            services.AddScoped<DbFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUser, UserService>();
            services.AddScoped<IApiKey, ApiKeyService>();
            return services;
        }


        /// <summary>ap
        /// Add instances of in-use services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        //public static IServiceCollection AddServices(this IServiceCollection services)
        //{
        //return services.AddScoped<IWorkService, WorkService>();
        //return services.AddScoped<IUser, UserService>();
        //return services.AddScoped<IApiKey, ApiKeyService>();
        //}


        public static async void ConfigApp(WebApplication app)
        {
            //app.UseMiddleware<ApiKeyMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}
