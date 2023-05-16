using Microsoft.EntityFrameworkCore;
using PeliculasApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Hacemos una inyeccion de dependecias
// el Data/AplicationDbContext es el que se pasa entre <>
// Dentro de GetConnectio queremos optener el DefaultConnection que esta dentro de appsetings.json
// Los demas metodos se instalaron en la libreria sqlserver
// Aqui capturamos toda la cadena de conexion y por la conexion de dependencias lo agregamos a la clase DbContext
builder.Services.AddDbContext<AplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Se instala otro paquete
// tools para hacer las migraciones para que los modelo se creen como tablas en SQL server
// Herramientas/Administrador de paquetes Nuget/Consola de administrador
// En la consola:
// add-migration Nombre [Enter]
// Se crea la migracion con los campos del modelo en la Base datos
// update-database [Ver los cambios en Sql Server]
// Se creo la Base de datos con su tabla Categoria

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
