namespace Basket.API.Exception;
public class BasketNotFoundException : NotFoundException
{
    public BasketNotFoundException(string username) :base("basket",username)
    {
        
    }

}

