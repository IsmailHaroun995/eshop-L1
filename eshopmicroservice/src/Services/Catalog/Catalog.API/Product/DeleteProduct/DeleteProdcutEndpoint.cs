
using Catalog.API.Product.GetProductByCategory;

namespace Catalog.API.Product.DeleteProduct;

public record DeleteProdcutRequest(Guid Id);
public record DeleteProductResponse(bool IsSucess);

internal class DeleteProdcutEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/product/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteProductCommand(id));
            var response = result.Adapt<DeleteProductResponse>();
            return Results.Ok(response);
        })
         .WithName("DeleteProduct")
        .Produces<DeleteProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("DeleteProduct")
        .WithDescription("DeleteProduct");
    }
}

