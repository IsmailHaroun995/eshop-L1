namespace Catalog.API.Product.GetProduct;

public record GetProductsRequest(int? PageNumber = 1 , int? Pagesize = 10);
public record GetProdcutsResponse(IEnumerable<Catalog.API.Models.Product> Products);
    public class GetProductEndpoint : ICarterModule
    {
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetProductsRequest request, ISender sender) =>

        {
            var query = request.Adapt<GetProductsQuery>();
            var result = await sender.Send(query);

            var response = result.Adapt<GetProdcutsResponse>();
            return response;
        })
        .WithName("GetProduct")
        .Produces<GetProdcutsResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("GetProduct")
        .WithDescription("Get Product");
    }
}

