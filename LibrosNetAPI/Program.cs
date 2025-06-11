using System.Text;
using LibrosNetAPI.Entidades;
using LibrosNetAPI.Servicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddTransient<IRepositorioAutores, RepositorioAutores>();
builder.Services.AddTransient<IRepositorioCategoria, RepositorioCategoria>();
builder.Services.AddTransient<IRepositorioEditorial, RepositorioEditorial>();
builder.Services.AddTransient<IRepositorioUsuarios, RepositorioUsuarios>();
builder.Services.AddTransient<IRepositorioLibros, RepositoriosLibros>();
builder.Services.AddTransient<IAlmacenadorArchivos, AlamacenarArchivosLocal>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
{
    opciones.UseSqlServer("name=DefaultConnection");
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opciones =>
{
    opciones.MapInboundClaims = false;

    opciones.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["llavejwt"]!)),
        ClockSkew = TimeSpan.Zero
    };

});

builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen(opciones =>
{
    opciones.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });


    opciones.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });

});


//Cors
var origenesPermitidos = builder.Configuration.GetSection("originesPermitidos").Get<string[]>();

builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(opcionesCors =>
    {
        opcionesCors.WithOrigins(origenesPermitidos).AllowAnyMethod().AllowAnyHeader()
        .WithExposedHeaders("cantidad-total-registros");

    });
});


var app = builder.Build();

//Middlewares
app.MapControllers();
app.UseCors();

app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();

app.Run();
