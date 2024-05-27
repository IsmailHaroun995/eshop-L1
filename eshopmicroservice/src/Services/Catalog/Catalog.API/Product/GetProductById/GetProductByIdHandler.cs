using Catalog.API.Exception;

namespace Catalog.API.Product.GetProductById;
public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
public record GetProductByIdResult(Catalog.API.Models.Product Product);

internal class GetProductByIdHandler
    (IDocumentSession session)
: IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Catalog.API.Models.Product>(query.Id, cancellationToken);
        if(product is null)
            throw new ProductNotFoundException(query.Id);
        
        return new GetProductByIdResult(product);
    }
}

