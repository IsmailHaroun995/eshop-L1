using Basket.API.Basket.GetBasket;
using System.Diagnostics.Eventing.Reader;

namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketRequest(string username);
public record DeleteBasketResponse(bool IsSucess);

public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{username}", async (string username, ISender sender) =>
        {
            var result = await sender.Send(new DeleteBasketCommand(username));
            var response = result.Adapt<DeleteBasketResponse>();
            return Results.Ok(response);
        }).WithName("DeleteBaske")
        .Produces<DeleteBasketResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("GetBDeleteBaskeasket")
        .WithDescription("DeleteBaske basket");
    }
}

