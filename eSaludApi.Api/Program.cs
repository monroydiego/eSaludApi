using eSaludApi.Application;
using eSaludApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// 1. Obtener cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Agregar servicios de las capas externas mediante los Extension Methods
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(connectionString!);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de CORS para tu Front en React Native / JS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFront",
        policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("PermitirFront");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();