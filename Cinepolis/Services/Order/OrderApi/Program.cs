using Microsoft.EntityFrameworkCore;
using OrderApi.Context;
using OrderApi.Repositories;
using OrderDomain.Worker;
using OrderDomain.Repositories;
using OrderDomain.Services;
using Prometheus;
using OrderApi.Startup;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Debug)
    .MinimumLevel.Override("System", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Debug)
    .Enrich.FromLogContext()


    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
    .CreateLogger();

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

builder.Services.ConfigAuthentication();
builder.Services.ConfigAuthorization();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
