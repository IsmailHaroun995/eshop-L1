namespace Ordering.Domain.Exception;
public class DomainException : System.Exception
{
   public DomainException(string msg):base($"Domain Excetion:\"{msg}\" throw from domain layer")
    {

    }
}
       
    

