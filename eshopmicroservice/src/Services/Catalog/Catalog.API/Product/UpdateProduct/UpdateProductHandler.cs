
namespace Catalog.API.Product.UpdateProduct;


public record UpdateProductCommand(Guid Id , string name , List<string> category , string description , decimal price , string imagefile)
    :ICommand<UpdateProdcutResult>;

public record UpdateProdcutResult(bool IsSucess);
public class UpdateProdcutCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProdcutCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("Product Id is required");
        RuleFor(c => c.name)
            .NotEmpty().WithMessage("Name is requird")
            .Length(2, 150).WithMessage("name must be between 2 and 150 char");

        RuleFor(c => c.price).GreaterThan(0).WithMessage("price must ne grater than 0 ");
    }
}





internal class UpdateProductHandler
    (IDocumentSession session)
    : ICommandHandler<UpdateProductCommand, UpdateProdcutResult>
{
    public async Task<UpdateProdcutResult> Handle(UpdateProductCommand command, 
        CancellationToken cancellationToken)
    {
        var prodcut = await session.LoadAsync<Catalog.API.Models.Product>(command.Id, cancellationToken);
        if(prodcut is null)
        {
            throw new ProductNotFoundException( command.Id);
        }
        prodcut.Name = command.name;
        prodcut.Category = command.category;
        prodcut.Description = command.description;
        prodcut.ImageFile = command.imagefile;
        prodcut.Price = command.price;
        

        session.Update(prodcut);
        await session.SaveChangesAsync(cancellationToken);


        return new UpdateProdcutResult(true);


    }
}

