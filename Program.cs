using HotelSOL.Data;
using HotelSOL.Repositories;
using HotelSOL.Repositorio;
using HotelSOL.Repositorio.Interfaces;
using HotelSOL.Repositorios.Interfaces;
using HotelSOL.Repositorios;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using HotelSOL.Client;



var builder = WebApplication.CreateBuilder(args);
AppContext.SetSwitch("System.Net.Security.SslStreamCertificateValidationIgnoreErrors", true);

// Configurar EF Core con SQL Server
builder.Services.AddDbContext<HotelContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HotelDB")));

// Repositorios registrados para inyección de dependencias
builder.Services.AddScoped<IRecepcionistaRepository, RecepcionistaRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IAdministradorRepository, AdministradorRepository>();
builder.Services.AddScoped<IAlojamientoRepository, AlojamientoRepository>();
builder.Services.AddScoped<ICalendarioRepository, CalendarioRepository>();
builder.Services.AddScoped<IFacturaRepository, FacturaRepository>();
builder.Services.AddScoped<IServicioRepository, ServicioRepository>();
builder.Services.AddScoped<IDetalleFacturaServicioRepository, DetalleFacturaServicioRepository>();
builder.Services.AddScoped<IReservaRepository, ReservaRepository>();

// Add Swagger & Controllers
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

// Configuración del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();


// Seed de datos iniciales SOLO si la base de datos está vacía
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<HotelContext>();
    await PoblarDB.InicializarAsync(context);
}

//Test conexion

var connStr = builder.Configuration.GetConnectionString("HotelDB");
Console.WriteLine("Cadena de conexión: " + connStr);

try
{
    using var conn = new SqlConnection(connStr);
    conn.Open();
    Console.WriteLine("✅ Conexión directa abierta con éxito");

    using var cmd = new SqlCommand("SELECT COUNT(*) FROM Usuarios", conn);
    var result = cmd.ExecuteScalar();
    Console.WriteLine("Total usuarios: " + result);
}
catch (Exception ex)
{
    Console.WriteLine("❌ Error de conexión directa: " + ex.Message);
}


app.Run();

