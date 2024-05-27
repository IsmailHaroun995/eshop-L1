using Marten.Linq.QueryHandlers;

namespace Catalog.API.Product.GetProductByCategory;

public record GetProdcutByCategoryQuery(string Category) :IQuery<GetProdutByCategoryResult>;
public record GetProdutByCategoryResult (IEnumerable<Catalog.API.Models.Product> Products);

internal class GetProdcutByCategoryHandler 
    (IDocumentSession session )
    : IQueryHandler<GetProdcutByCategoryQuery, GetProdutByCategoryResult>
{
    public async Task<GetProdutByCategoryResult> Handle(GetProdcutByCategoryQuery query,
        CancellationToken cancellationToken)
    {

        var products = await session.Query<Catalog.API.Models.Product>()
            .Where(p => p.Category.Contains(query.Category))
            .ToListAsync(cancellationToken);


        return new GetProdutByCategoryResult(products);
    }
}

