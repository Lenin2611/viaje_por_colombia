using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Application.UnitOfWork;
using AspNetCoreRateLimit;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence.Data;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Test Api",
        Version = "v1"
    });
});

builder.Services.AddProblemDetails();

builder.Services.AddApiVersioning(option =>
{
    option.DefaultApiVersion = new ApiVersion(1,0);
    option.AssumeDefaultVersionWhenUnspecified = true;
    option.ReportApiVersions = true;
});

builder.Services.AddRouting(option =>
    option.LowercaseUrls = true
);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddHttpClient();

builder.Services.AddDbContext<ViajeContext>(option =>
{
    string connectionString = builder.Configuration.GetConnectionString("MySql");
    option.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddControllers().AddJsonOptions(option =>
{
    option.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddCors(option => {
    option.AddPolicy("CorsPolicy", option => {
        option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddMemoryCache();

builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

builder.Services.AddInMemoryRateLimiting();

builder.Services.Configure<IpRateLimitOptions>(option => {
    option.EnableEndpointRateLimiting = true;
    option.StackBlockedRequests = false;
    option.RealIpHeader = "X-Real-IP";
    option.GeneralRules = new List<RateLimitRule>
    {
        new RateLimitRule
        {
            Endpoint = "*",
            Period = "10s",
            Limit = 10
        }
    };
});

builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseIpRateLimiting();

app.UseHttpsRedirection();

app.UseStatusCodePages();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();