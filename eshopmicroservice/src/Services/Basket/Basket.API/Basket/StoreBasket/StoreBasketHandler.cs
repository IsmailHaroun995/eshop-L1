
using Discount.Grpc;

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

public  class StoreBasketCommandHandler(IBasketRepository repository , DiscountProtoServoce.DiscountProtoServoceClient discountProto) :
    ICommandHandler<StoreBasktCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasktCommand command, CancellationToken cancellationToken)
    {

        await DetectDiscount(command.Cart,cancellationToken);
        ShoppingCart cart = command.Cart;
        await repository.StoreBasket(command.Cart, cancellationToken);
        return new StoreBasketResult(command.Cart.UserName);
    }
    public async Task DetectDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    {

        foreach (var item in cart.Items)
        {
            var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken);
            item.Price -= coupon.Price;
        }
    }
}

