using Microsoft.EntityFrameworkCore;
using OrderApi.Context;
using OrderApi.Repositories;
using OrderDomain.Worker;
using OrderDomain.Repositories;
using OrderDomain.Services;
using Microsoft.Identity.Client;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseKestrel(so =>
{
    so.Limits.MaxConcurrentConnections = 100000;
    so.Limits.MaxConcurrentUpgradedConnections = 100000;
    so.Limits.MaxRequestBodySize = 52428800;
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<PaymentConsumer>();
builder.Services.AddHealthChecks();


string connectionString = builder.Configuration.GetConnectionString("OrderDB");
builder.Services.AddDbContext<OrderContext>(options => options.UseSqlServer(connectionString));
//builder.Services.AddDbContext<OrderContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();


var app = builder.Build();

//using (var scope = app.Services.CreateAsyncScope())
//{
//    using (var db = scope.ServiceProvider.GetService<OrderContext>())
//    {
//        db.Database.MigrateAsync();
//    }
//}
using var scope = app.Services.CreateAsyncScope();
using var db = scope.ServiceProvider.GetService<OrderContext>();
db.Database.MigrateAsync();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMetricServer();
app.UseHealthChecks("/health");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
