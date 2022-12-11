using HumanResourceAPI.Infrastructure;
using LoggerService;

namespace HumanResourceAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options => 
            {
                options.AddPolicy("CorsPolicy", builder => 
                {
                    builder.AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader();
                });
            });

        public static void ConfigureIISIntergration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {
                
            });

        public static void ConfigureLoggingService(this IServiceCollection services) =>
            services.AddScoped<ILoggerManager, LoggerManager>();
    }
}
