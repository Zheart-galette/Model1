using Microsoft.EntityFrameworkCore;
using Model1.Models;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions
    .PropertyNamingPolicy = JsonNamingPolicy.CamelCase);

builder.Services.AddDbContext<MapDbContext>( o => o.UseSqlServer(builder.Configuration.GetConnectionString("MapDb")));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
app.MapGet("/ping",(MapDbContext db) => db.Users.Count());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
