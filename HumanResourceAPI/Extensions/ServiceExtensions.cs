using Entities;
using HumanResourceAPI.Infrastructure;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Repository;

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

        public static void ConfigureSqlContext(this IServiceCollection services,
            IConfiguration configuration) =>
            services.AddDbContext<AppDbContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"), b =>
                b.MigrationsAssembly("HumanResourceAPI")));

        public static void ConfigureRepository(this IServiceCollection services)
            => services.AddTransient(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));
         
    }
}
