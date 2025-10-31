using CashFlow.Application;
using CashFlow.Infrastructure;
using CashFlow.Api.Middleware;
using CashFlow.Api.Filters;
using CashFlow.Infrastructure.Migrations;
using CashFlow.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Api.Token;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using CashFlow.Infrastructure.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.MapType<DateOnly>(() => new Microsoft.OpenApi.Models.OpenApiSchema
    {
        Type = "string",
        Format = "date"
    });
    config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Digite Bearer <seu_token>",
        In = ParameterLocation.Header,
        Scheme = "Bearer",
        Type = SecuritySchemeType.ApiKey,
    });
    config.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference{
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddInfrastructure(builder.Configuration); // gerado via classe estática em infrastructure/DependencyInjectionExtension
builder.Services.AddApplication();

builder.Services.AddScoped<ITokenProvider, HttpContextTokenValue>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = new TimeSpan(0),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Settings:Jwt:SigningKey")!)),
    };
});

builder.Services.AddHealthChecks().AddDbContextCheck<CashFlowDBContext>();

var app = builder.Build();

app.MapHealthChecks("/Health", new HealthCheckOptions
{
    AllowCachingResponses = false,
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
    }
});

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<CultureMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

if (builder.Configuration.IsTestEnvinronment() == false)
{
    await MigrateDatabase();
}

app.Run();

async Task MigrateDatabase()
{
    await using var scope = app.Services.CreateAsyncScope();

    await DatabaseMigration.MigrateDatabase(scope.ServiceProvider);
}

public partial class Program { }