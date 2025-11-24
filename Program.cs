using ContabilidadSantaCruz.Infraestructura.Data;
using ContabilidadSantaCruz.Infraestructura.Repositorios;
using ContabilidadSantaCruz.Core.Servicios;
using ContabilidadSantaCruz.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using ContabilidadSantaCruz.Infraestructura;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
/*
var connectionString = Environment.GetEnvironmentVariable("DATABASE_PUBLIC_URL")
    ?? builder.Configuration.GetConnectionString("DefaultConnection");

Console.WriteLine($"[INFO] Usando conexión: {(string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DATABASE_PUBLIC_URL")) ? "Local" : "Railway")}");

builder.Services.AddDbContext<ContabilidadContext>(options =>
    options.UseNpgsql(connectionString)
);*/

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
/*
builder.Services.AddScoped<ICuentaBancariaRepositorio, CuentaBancariaRepositorio>();
builder.Services.AddScoped<IFacturaRepositorio, FacturaRepositorio>();
builder.Services.AddScoped<IPresupuestoRepositorio, PresupuestoRepositorio>();
builder.Services.AddScoped<IPagoRepositorio, PagoRepositorio>();
builder.Services.AddScoped<IProveedorRepositorio, ProveedorRepositorio>();
builder.Services.AddScoped<ITransaccionRepositorio, TransaccionRepositorio>();

builder.Services.AddScoped<IGestionCuentasBancariasService, GestionCuentasBancariasService>();
builder.Services.AddScoped<IGestionFacturasService, GestionFacturasService>();
builder.Services.AddScoped<IGestionPresupuestosService, GestionPresupuestosService>();
builder.Services.AddScoped<IGestionPagosService, GestionPagosService>();
builder.Services.AddScoped<IGestionProveedoresService, GestionProveedoresService>();*/

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.UseUrls("http://0.0.0.0:8080");

var app = builder.Build();
/*
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ContabilidadContext>();
    try
    {
        db.Database.Migrate();
        Console.WriteLine("--> Migración aplicada exitosamente en Railway.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"--> Error aplicando migración: {ex.Message}");
    }
}*/

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.MapControllers();

Console.WriteLine("[INFO] Servidor ejecutándose en http://0.0.0.0:8080");
await app.RunAsync();
