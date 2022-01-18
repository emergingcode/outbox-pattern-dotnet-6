using OrderManagementApi.Business;
using OrderManagementApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/order", () =>
{
    OrderManagementService orderManagement = new();

    var newOrder = new Order
    {
        Total = 500,
        TotalItems = 2,
        CustomerName = "Will",
        CreatedAt = new DateTime(2021, 10, 15)
    };


    newOrder.AddItem(new OrderItem
    {
        ProductId = 1,
        Quantity = 2,
    });

    newOrder.AddItem(new OrderItem
    {
        ProductId = 2,
        Quantity = 3,
    });

    orderManagement.PlaceOrder(newOrder);
})
.WithName("OrderManagement");

app.Run();