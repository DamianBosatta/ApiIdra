using EsMasBarato.EF.Context;
using EsMasBarato.Negocios.Unidad_De_Trabajo;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<ContextEsMasBarato>(mysqlBuilder =>
//{
    
//});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajo>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

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
