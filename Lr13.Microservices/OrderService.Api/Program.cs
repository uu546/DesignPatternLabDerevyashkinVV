using OrderService.Api.HttpClients;
using OrderService.Api.Repositories;
using OrderService.Api.Sagas;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<OrderRepository>();

builder.Services.AddHttpClient<PaymentClient>();

builder.Services.AddScoped<CheckoutSaga>();

var app = builder.Build();

app.MapControllers();

app.Run();
