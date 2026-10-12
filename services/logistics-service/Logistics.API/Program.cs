using Logistics.Application.Interfaces;
using Logistics.Infrastructure.Data;
using Logistics.Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Logistics.Application.Consumers;

var builder = WebApplication.CreateBuilder(args);

// 1. Database Connection 
builder.Services.AddDbContext<LogisticsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Repositories Register  (Dependency Injection)
builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
builder.Services.AddScoped<IRiderRepository, RiderRepository>();

// 3. RabbitMQ / MassTransit 
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<PaymentCompletedEventConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h => {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.MapHub<Logistics.API.Hubs.LocationHub>("/location-hub");
app.Run();