using Localdorateste.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RentCarSys.Application.Interfaces;
using RentCarSys.Application.Repository;
using RentCarSys.Application.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});


builder.Services.AddScoped<IClientesRepository, ClientesRepository>();
builder.Services.AddScoped<IVeiculosRepository, VeiculosRepository>();
builder.Services.AddScoped<IReservasRepository, ReservasRepository>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<VeiculoService>();


builder.Services.AddEndpointsApiExplorer();

#region Swagger
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo { Title = "Leafcutter Cloud", Version = "V1" });
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter JWT Bearer authorisation token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "Bearer {token}",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    s.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);
    s.AddSecurityRequirement(new OpenApiSecurityRequirement { { securityScheme, Array.Empty<string>() } });
});
#endregion

builder.Services.AddDbContext<Contexto>
    (options => options.UseSqlServer
    ("Data Source=DESKTOP-4S977G0;Initial Catalog=RentCarSys;Integrated Security=True; TrustServerCertificate=True"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
