
using Basket.API.Basket.StoreBasket;

namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketCommand(string username) : ICommand<DeleteBasketResult>;
public record DeleteBasketResult(bool IsSucess);


public class DeleteBasketCommandValidor : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidor()
    {
        RuleFor(x => x.username).NotEmpty().WithMessage("Username is requird");
    }
}

public class DeleteBasketCommandHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
     
        await repository.DeleteBasket(command.username, cancellationToken);
        return new DeleteBasketResult(true);
    }
}

