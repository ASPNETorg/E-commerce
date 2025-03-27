using E_commerce.Infrastructure.Data;
using E_commerce.Infrastructure.Models.Services.Contracts;
using E_commerce.Infrastructure.Repositories;
using E_commerce.Application.Contracts;
using Microsoft.EntityFrameworkCore;
using E_commerce.Application.Services.ProductService;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

#region [- AddDbContext() -]
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<ProjectDbContext>(options => options.UseSqlServer(connectionString));
#endregion

#region [- Models IOC -]
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
#endregion

#region [- ApplicationServices IOC -]
builder.Services.AddScoped<IProductService, ProductService>();
#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
