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

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserService, UserService>();

// Add services to the container.
builder.Services.AddDbContext<GeoSolutionsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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

// ✅ Configurar CORS para permitir qualquer origem, método e cabeçalho
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();

// Register repositories
builder.Services.AddScoped<IServiceTypeRepository, ServiceTypeRepository>();
builder.Services.AddScoped<IIntentionServiceRepository, IntentionServiceRepository>();
builder.Services.AddScoped<IBudgetRepository, BudgetRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IConfrontationRepository, ConfrontationRepository>();
builder.Services.AddScoped<IDistanceRepository, DistanceRepository>();
builder.Services.AddScoped<IHostingRepository, HostingRepository>();
builder.Services.AddScoped<IStartPointRepository, StartPointRepository>();

// Register services
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

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Refit client
builder.Services
    .AddRefitClient<IDistanceApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.exemplo.com"));

var app = builder.Build();

// Pipeline
if (app.Environment.EnvironmentName.Contains(Environments.Development))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

// ✅ Aplicar política CORS corretamente
app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
