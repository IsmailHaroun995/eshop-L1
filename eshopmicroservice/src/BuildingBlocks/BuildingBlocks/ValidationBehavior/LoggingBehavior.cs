using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.ValidationBehavior;
public class LoggingBehavior<TRequest, TResponse> 
    (ILogger<LoggingBehavior<TRequest,TResponse>> logger )
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull ,IRequest<TResponse>
    where TResponse :notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("[Start] Handle request ={Request} - Response {Response} - RequsetData={RequestData}",
            typeof(TRequest).Name , typeof(TResponse).Name , request);
        var timer = new Stopwatch();
        timer.Start();
        var response = await next();
        timer.Stop();
        var timeTaker = timer.Elapsed;
        if (timeTaker.Seconds > 3)
            logger.LogWarning("[PERFORMANCE] the request {Request} took {TimeTaken}", typeof(TRequest).Name, timeTaker.Seconds);

        logger.LogInformation("[End] the request finished");
        return response;
    }
}

