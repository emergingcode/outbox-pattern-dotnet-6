using SampleOutboxPattern.Orders.Application;
using SampleOutboxPattern.Orders.Application.RequestModels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/order", async (CustomerOrderRequest customerOrderRequest) =>
{
    OrderService orderService = new();

    await orderService.PlaceCustomerOrder(customerOrderRequest);
});

app.Run();