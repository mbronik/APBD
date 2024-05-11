using zad4.Repositories;
using zad4.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddScoped<WarehouseRepository, WarehouseRepositoryImpl>();
builder.Services.AddScoped<ProductRepository, ProductRepositoryImpl>();
builder.Services.AddScoped<OrderRepository, OrderRepositoryImpl>();
builder.Services.AddScoped<ProductWarehouseRepository, ProductWarehouseRepositoryImpl>();
builder.Services.AddScoped<WarehouseService, WarehouseServiceImpl>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();