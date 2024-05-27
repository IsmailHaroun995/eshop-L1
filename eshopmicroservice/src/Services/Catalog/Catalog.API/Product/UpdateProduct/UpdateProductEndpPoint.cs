
using Catalog.API.Product.GetProductById;

namespace Catalog.API.Product.UpdateProduct;

public record UpdateProductRequest(Guid Id, string name, List<string> category, string description, decimal price, string imagefile);
public record UpdateProdcutResponse(bool IsSuccess);

public class UpdateProductEndpPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("products",
            async (UpdateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateProductCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<UpdateProdcutResponse>();
                return Results.Ok(response);
            })
         .WithName("UpdateProdcut")
        .Produces<UpdateProdcutResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("UpdateProdcut")
        .WithDescription("UpdateProdcut ");
    }
}

