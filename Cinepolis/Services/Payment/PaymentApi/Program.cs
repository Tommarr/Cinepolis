using Microsoft.EntityFrameworkCore;
using OrderApi.Repositories;
using OrderApi.Services;
using PaymentApi.Context;
using PaymentDomain.Repositories;
using PaymentApi.Worker;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();


string connectionString = builder.Configuration.GetConnectionString("PaymentDB");
builder.Services.AddDbContext<PaymentContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Scoped);

builder.Services.AddScoped<IPaymentService , PaymentService>();
builder.Services.AddScoped<IPaymentRepository , PaymentRepository>();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository , OrderRepository>();

builder.Services.AddHostedService<OrderConsumer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMetricServer();
app.UseHealthChecks("/health");

app.UseAuthorization();

app.MapControllers();

app.Run();
