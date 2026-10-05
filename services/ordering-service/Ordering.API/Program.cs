using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ordering.Application.Features.Orders.Commands.CreateOrder;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// 1. Add Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. Register MediatR (Application Layer)
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommand).Assembly));

// 3. Register FluentValidation (Application Layer)
builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderCommandValidator>();

// 4. Register Infrastructure Services (PostgreSQL, Redis, Repositories)
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();