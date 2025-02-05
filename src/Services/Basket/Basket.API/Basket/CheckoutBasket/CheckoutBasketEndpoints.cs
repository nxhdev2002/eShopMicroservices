﻿namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketRequest(BasketCheckoutDto Input);

public record CheckoutBasketResponse(bool IsSuccess);

public class CheckoutBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket/checkout", async (CheckoutBasketRequest request, ISender sender) =>
        {
            var command = request.Input.Adapt<CheckoutBasketCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CheckoutBasketResponse>();
            return Results.Ok(response);
        })
        .WithName("CheckoutBasket")
        .Produces<CheckoutBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Checkout Basket")
        .WithDescription("Checkout Basket");
    }
}
