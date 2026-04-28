using ApplicationApp.Interfaces;
using ApplicationApp.OpenApp;
using Domain.Interfaces.Generics;
using Domain.Interfaces.InterfaceCarro;
using Domain.Interfaces.InterfaceCliente;
using Domain.Interfaces.InterfaceAlocacao;
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
builder.Services.AddScoped<IClienteApp, AppGenerica<Cliente>>();
builder.Services.AddScoped<IAlocacaoApp, AppGenerica<Alocacao>>();

// repositórios também precisam ser registrados
builder.Services.AddScoped<ICarro, RepositoryCarro>();
builder.Services.AddScoped<ICliente, RepositoryCliente>(); // ainda não existe
builder.Services.AddScoped<IAlocacao, RepositoryAlocacao>();

// services do domain
builder.Services.AddScoped<IServiceCarro, CarroService>();
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
