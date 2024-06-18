namespace Basket.API.Basket.GetBasket;
public record GetBasketRequet(String username);
public record GetBasketResponse(ShoppingCart card);
public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{username}", async (string username, ISender sender) =>
        {
            var result = await sender.Send(new GetBaketQuery(username));
            var response = result.Adapt<GetBasketResponse>();
            return Results.Ok(response);
        })
         .WithName("GetBasket")
        .Produces<GetBasketResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("GetBasket")
        .WithDescription("GetBasket basket");
    }
}

