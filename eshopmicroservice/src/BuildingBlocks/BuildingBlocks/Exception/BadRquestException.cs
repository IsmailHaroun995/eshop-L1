namespace BuildingBlocks.Exception;

public class BadRquestException :System.Exception
    {
    public BadRquestException(string message) :base(message)
    {    
    }
    public BadRquestException(string message , string  details) :base(message)
    {
        Details = details;
    }
    public string? Details {  get;}
}

