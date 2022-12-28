using HumanResourceAPI.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using NLog;

var builder = WebApplication.CreateBuilder(args);

//Config logging

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntergration();
builder.Services.ConfigureLoggingService();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepository();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.Configure<ApiBehaviorOptions>(options =>
    options.SuppressModelStateInvalidFilter = true
);

builder.Services.AddControllers(config => { 
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureVersioning();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("v1/swagger.json", "Human Resource API v1");
        s.SwaggerEndpoint("v2/swagger.json", "Human Resource API v2");
    });
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseForwardedHeaders(new ForwardedHeadersOptions 
{
   ForwardedHeaders = ForwardedHeaders.All
});

app.UseAuthorization();

app.MapControllers();

app.Run();
