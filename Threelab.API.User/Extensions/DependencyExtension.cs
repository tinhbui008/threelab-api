using Microsoft.EntityFrameworkCore;
using Threelab.Infrastructure.Context;

namespace Threelab.API.User.Extensions
{
    public static class DependencyExtension
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddDbContext<DBContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("AccountConnection"),
                    sqlOptions => sqlOptions.CommandTimeout(120));
                options.UseLazyLoadingProxies();
            });

            //services.AddTransient<Func<DBContext>>((provider) => () => provider.GetService<DBContext>());
            //services.AddTransient<DbFactory>();
            //services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddAuthorization();
            //services.AddTransient<IUser, UserService>();
            //services.AddTransient<IApiKey, ApiKeyService>();
            return services;
        }

        public static async void ConfigApp(WebApplication app)
        {
            //app.UseMiddleware<ApiKeyMiddleware>();
            if (app.Environment.IsDevelopment())
            {
            }
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseHttpsRedirection();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseAuthorization();
            app.MapControllers();
            app.Run();

        }
    }
}
