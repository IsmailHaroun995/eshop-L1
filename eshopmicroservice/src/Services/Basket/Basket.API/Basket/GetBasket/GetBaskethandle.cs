using Basket.API.Data;

namespace Basket.API.Basket.GetBasket;
public record GetBaketQuery(string userName): IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart Cart);
public class GetBasketQueryhandle(IBasketRepository repository) : IQueryHandler<GetBaketQuery, GetBasketResult>
{
    public async  Task<GetBasketResult> Handle(GetBaketQuery query, CancellationToken cancellationToken)
    {
        var basket  = await repository.GetBasket(query.userName);
        return new GetBasketResult(basket);
    }
}

