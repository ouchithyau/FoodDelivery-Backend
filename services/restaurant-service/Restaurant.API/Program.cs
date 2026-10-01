using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Interfaces;
using Restaurant.Application.Services;
using Restaurant.Infrastructure.Data;
using Restaurant.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// 1. Register PostgreSQL DbContext
builder.Services.AddDbContext<RestaurantDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Register Repositories (Data Access Layer)
builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();

// 3. Register Services (Business Logic Layer)
builder.Services.AddScoped<RestaurantService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<MenuItemService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/asp.netcore/swashbuckle
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