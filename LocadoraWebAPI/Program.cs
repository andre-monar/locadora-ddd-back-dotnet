using ApplicationApp.Interfaces;
using ApplicationApp.OpenApp;
using Domain.Interfaces.Generics;
using Domain.Interfaces.InterfaceAlocacao;
using Domain.Interfaces.InterfaceCarro;
using Domain.Interfaces.InterfaceCategoriaCarro;
using Domain.Interfaces.InterfaceCliente;
using Domain.Interfaces.InterfaceServices;
using Domain.Services;
using Entities.Entities;
using Infrastructure.Repository.Generics;
using Infrastructure.Repository.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI
builder.Services.AddScoped<ICarroApp, CarroApp>();
builder.Services.AddScoped<IClienteApp, GenericApp<Cliente>>();
builder.Services.AddScoped<IAlocacaoApp, GenericApp<Alocacao>>();
builder.Services.AddScoped<ICategoriaCarro, RepositoryCategoriaCarro>();
builder.Services.AddScoped<ICategoriaCarroApp, GenericApp<CategoriaCarro>>();
builder.Services.AddScoped<ICategoriaCarroService, CategoriaCarroService>();

// repositórios também precisam ser registrados
builder.Services.AddScoped<ICarro, CarroRepository>();
builder.Services.AddScoped<ICliente, RepositoryCliente>(); // ainda não existe
builder.Services.AddScoped<IAlocacao, RepositoryAlocacao>();

// services do domain
builder.Services.AddScoped<ICarroService, CarroService>();
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
