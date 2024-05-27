namespace Catalog.API.Product.GetProductByCategory;

public record GetProductByCategoryResponse(IEnumerable<Catalog.API.Models.Product> Products);
public class GetProductByCatrgoryEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/cateogry/{category}", async (string category, ISender sender) =>
        {
            var result = await sender.Send(new GetProdcutByCategoryQuery(category));
            var response = result.Adapt<GetProductByCategoryResponse>();

            return Results.Ok(response);

        })
         .WithName("GetProductByCategory")
        .Produces<GetProductByCategoryResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("GetProductByCategory")
        .WithDescription("GetProductByCategory");
    }
}
