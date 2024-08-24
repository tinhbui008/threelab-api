using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Threelab.Domain.Entities;
using Threelab.Domain.Interfaces;
using Threelab.Domain.Interfaces.Services;
using Threelab.Infrastructure;
using Threelab.Infrastructure.Context;
using Threelab.Infrastructure.Middlewares;
using Threelab.Service.Services;

namespace Threelab.API.Account.Extensions
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
            return services;
        }


        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IUser, UserService>();
            return services;
        }


        public static async void ConfigApp(WebApplication app)
        {
            app.UseMiddleware<ApiKeyMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}
