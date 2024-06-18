
namespace Basket.API.Basket.StoreBasket;
public record StoreBasktCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string username);

public class StoreBasketCommandValidater : AbstractValidator<StoreBasktCommand>
{
    public StoreBasketCommandValidater()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart Can not be null");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("Username is requird");
    }

}

public  class StoreBasketCommandHandler(IBasketRepository repository) :
    ICommandHandler<StoreBasktCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasktCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart cart = command.Cart;

        await repository.StoreBasket(command.Cart, cancellationToken);


        return new StoreBasketResult(command.Cart.UserName);
    }
}

