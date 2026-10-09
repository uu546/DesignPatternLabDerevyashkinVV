using OrderService.Api.HttpClients;
using OrderService.Api.Models;
using OrderService.Api.Sagas;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<List<Order>>();

builder.Services.AddHttpClient<PaymentClient>();

builder.Services.AddScoped<CheckoutSaga>();

var app = builder.Build();

app.MapControllers();

app.Run();
