namespace Catalog.API.Product.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool IsSucess);

public class DeleteCommandValidater:AbstractValidator<DeleteProductCommand>
{
    public DeleteCommandValidater()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("Id is required");
    }
}


internal class DeleteProdcutHandler(IDocumentSession session) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand request, 
        CancellationToken cancellationToken)
    {
        session.Delete<Models.Product>(request.Id);
        await session.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);

    }
}



