
namespace Catalog.API.Product.GetProduct;
public record GetProductsQuery(int? PageNumber = 1, int? Pagesize = 10) : IQuery<GetProductsResult>;
public record GetProductsResult(IEnumerable<Catalog.API.Models.Product> Products);
internal class GetProductQueryHandler(IDocumentSession session )
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {

            var products = await session.Query<Catalog.API.Models.Product>()
            .ToPagedListAsync(query.PageNumber ??1 , query.Pagesize ??10, cancellationToken);
        return new GetProductsResult(products);
    }
}

