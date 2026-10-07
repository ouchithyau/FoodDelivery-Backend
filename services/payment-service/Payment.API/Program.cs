using Microsoft.EntityFrameworkCore;
using Payment.Infrastructure.Data;
using Payment.Application.Interfaces;
using Payment.Infrastructure.Repositories;
using Payment.Infrastructure.Gateways;
using MassTransit;
using Payment.Application.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PaymentDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentGateway, StripePaymentGateway>();

// RabbitMQ & MassTransit Configuration
builder.Services.AddMassTransit(config =>
{
   
    config.AddConsumer<OrderPlacedEventConsumer>();

    config.UsingRabbitMq((context, configurator) =>
    {

        configurator.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        configurator.ConfigureEndpoints(context);


        configurator.ReceiveEndpoint("order-placed-queue", e =>
        {
            e.ConfigureConsumer<OrderPlacedEventConsumer>(context);
        });
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();