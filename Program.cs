using GeoSolucoesAPI.Integration.DistanceAPI;
using GeoSolucoesAPI.Models;
using GeoSolucoesAPI.Services;
using GeoSolucoesAPI.Services.Interfaces;

using GeoSolucoesAPI.Repositories;
using GeoSolucoesAPI.Repositories.Interfaces;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Refit;
using ServiceManagement.Repositories;
using ServiceManagement.Services;
using System.Text;

using static GeoSolucoesAPI.Controllers.UsersController;
using GeoSolucoesAPI.Middlewares;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
QuestPDF.Settings.License = LicenseType.Community;

// Repositórios
builder.Services.AddScoped<IServiceTypeRepository, ServiceTypeRepository>();
builder.Services.AddScoped<IIntentionServiceRepository, IntentionServiceRepository>();
builder.Services.AddScoped<IBudgetRepository, BudgetRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IConfrontationRepository, ConfrontationRepository>();
builder.Services.AddScoped<IDistanceRepository, DistanceRepository>();
builder.Services.AddScoped<IHostingRepository, HostingRepository>();
builder.Services.AddScoped<IStartPointRepository, StartPointRepository>();

// Serviços
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IServiceTypeService, ServiceTypeService>();
builder.Services.AddScoped<IIntentionServiceService, IntentionServiceService>();
builder.Services.AddScoped<IBudgetService, BudgetService>();
builder.Services.AddScoped<IGeoLocationService, GeoLocationService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IConfrontationService, ConfrontationService>();
builder.Services.AddScoped<IDistanceService, DistanceService>();
builder.Services.AddScoped<IHostingService, HostingService>();
builder.Services.AddScoped<IStartPointService, StartPointService>();
builder.Services.AddScoped<IValidationService, ValidationService>();

// DbContext
builder.Services.AddDbContext<GeoSolutionsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// JWT
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Ry74cBQva5dThwbwchR9jhbtRFnJxWSZ"))
        };
    });
builder.Services.AddAuthorization();

// CORS - Corrigido
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Refit - API externa
builder.Services
    .AddRefitClient<IDistanceApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.exemplo.com"));

// Controllers
builder.Services.AddControllers();

var app = builder.Build();

// Middleware de exceção
app.UseMiddleware<ExceptionMiddleware>();

// Swagger
if (app.Environment.EnvironmentName.Contains(Environments.Development))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Ordem correta de middlewares
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
