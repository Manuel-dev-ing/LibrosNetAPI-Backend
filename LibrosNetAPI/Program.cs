using LibrosNetAPI.Entidades;
using LibrosNetAPI.Servicios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddTransient<IRepositorioAutores, RepositorioAutores>();
builder.Services.AddTransient<IRepositorioCategoria, RepositorioCategoria>();

builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
{

    opciones.UseSqlServer("name=DefaultConnection");

});


var app = builder.Build();

app.MapControllers();

app.Run();
