using Polly;

namespace ProductServices.Services.Generic;

public class RetryDecorator<TCommand> : ICommandService<TCommand>
{
    private readonly ICommandService<TCommand> _service;
    private Policy _policy;
    private int _maxRetry = 3;

    public RetryDecorator(ICommandService<TCommand> service)
    {
        _service = service;
        _policy = Policy.Handle<BadHttpRequestException>().WaitAndRetry(_maxRetry, retryAattempt => TimeSpan.FromSeconds(Math.Pow(2, retryAattempt)));
    }

    public void Execute(TCommand command)
    {
        try
        {
            _policy.Execute(() => _service.Execute(command));
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}