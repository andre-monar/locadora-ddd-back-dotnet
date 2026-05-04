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
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Infrastructure.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ── DbContext ────────────────────────────────────────────────────
builder.Services.AddDbContext<ContextBase>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ── Repositório genérico ─────────────────────────────────────────
// Necessário para o GenericApp<T> funcionar via DI
builder.Services.AddScoped(typeof(IGeneric<>), typeof(GenericRepository<>));

// ── Repositórios específicos ─────────────────────────────────────
builder.Services.AddScoped<ICarro, CarroRepository>();
builder.Services.AddScoped<ICliente, ClienteRepository>();
builder.Services.AddScoped<IAlocacao, AlocacaoRepository>();
builder.Services.AddScoped<ICategoriaCarro, CategoriaCarroRepository>();

// ── Domain Services ──────────────────────────────────────────────
builder.Services.AddScoped<ICarroService, CarroService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IAlocacaoService, AlocacaoService>();
builder.Services.AddScoped<ICategoriaCarroService, CategoriaCarroService>();

// ── Application ──────────────────────────────────────────────────
builder.Services.AddScoped<ICarroApp, CarroApp>();
builder.Services.AddScoped<IClienteApp, ClienteApp>();
builder.Services.AddScoped<IAlocacaoApp, AlocacaoApp>();
builder.Services.AddScoped<ICategoriaCarroApp, CategoriaCarroApp>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
    )
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<ContextBase>();
        db.Database.Migrate();
    }
}
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();