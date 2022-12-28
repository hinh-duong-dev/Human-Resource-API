using Entities;
using HumanResourceAPI.Infrastructure;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Repository;
using Microsoft.OpenApi.Models;
using HumanResourceAPI.Controllers;

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
            => services.AddTransient(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>))
            .AddTransient<IRepositoryManager, RepositoryManager>();


        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s => 
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                { 
                    Title = "Human Resource API",
                    Version = "v1"
                });

                s.SwaggerDoc("v2", new OpenApiInfo
                {
                    Title = "Human Resource API",
                    Version = "v2"
                });
            });
        }

        public static void ConfigureVersioning(this IServiceCollection services) =>
            services.AddApiVersioning(opt => 
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ApiVersionReader = new HeaderApiVersionReader("api-version");
                opt.Conventions.Controller<CompaniesController>().HasApiVersion(new ApiVersion(1,0));
                opt.Conventions.Controller<CompaniesV2Controller>().HasApiVersion(new ApiVersion(2, 0));
            });
         
    }
}
