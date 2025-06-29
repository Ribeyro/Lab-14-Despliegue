using System.Text;
using Hangfire;
using Hangfire.MySql;
using Lab11.Application.Configuration;
using Lab11.Infrastructure.Configuration;
using Lab.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Hangfire.MySql;

var builder = WebApplication.CreateBuilder(args);

// 🔧 Servicios de infraestructura y aplicación
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);

// 📨 Servicio para pruebas de Hangfire
builder.Services.AddTransient<NotificationService>();
builder.Services.AddControllers();

// 🧠 Hangfire (usando MySQL)

builder.Services.AddHangfire(config =>
{
    config.UseStorage(
        new MySqlStorage(
            builder.Configuration.GetConnectionString("DefaultConnection")!,
            new MySqlStorageOptions()  // 👈 se requiere aunque no uses opciones
        )
    );
});


builder.Services.AddTransient<CleanupService>();

builder.Services.AddHangfireServer();

// 🔐 JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!);
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });
builder.Services.AddAuthorization();

// 📘 Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Lab11.API",
        Version = "v1",
        Description = "API para gestión de usuarios (Lab11)"
    });
});

// 🚀 Build App
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Hangfire Dashboard
app.UseHangfireDashboard("/hangfire");

//Job recurrente
RecurringJob.AddOrUpdate<NotificationService>(
    "job-notificacion-diaria",
    service => service.SendNotification("usuario_diario"),
    Cron.Daily);

RecurringJob.AddOrUpdate<CleanupService>(
    "job-limpieza-tickets",
    service => service.CleanupOldTickets(),
    Cron.Weekly); // o Cron.Daily si prefieres


app.MapControllers();
app.Run();