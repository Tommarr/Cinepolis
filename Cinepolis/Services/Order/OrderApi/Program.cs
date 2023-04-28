using Microsoft.EntityFrameworkCore;
using OrderApi.Context;
using OrderApi.Repositories;
using OrderDomain.Worker;
using OrderDomain.Repositories;
using OrderDomain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<PaymentConsumer>();


string connectionString = builder.Configuration.GetConnectionString("OrderDB");
builder.Services.AddDbContext<OrderContext>(options => options.UseSqlServer(connectionString));
//builder.Services.AddDbContext<OrderContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();


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
