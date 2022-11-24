using Polly;

namespace ProductServices.Services.Generic;

public class RetryDecorator<TCommand> : ICommandService<TCommand>
{
    private readonly ICommandService<TCommand> _inner;
    private Policy _policy;
    private int _maxRetry = 3;

    public RetryDecorator(ICommandService<TCommand> inner)
    {
        _inner = inner;
        _policy = Policy.Handle<BadHttpRequestException>().WaitAndRetry(_maxRetry, retryAattempt => TimeSpan.FromSeconds(Math.Pow(2, retryAattempt)));
    }

    public void Execute(TCommand command)
    {
        try
        {
            _policy.Execute(() => _inner.Execute(command));
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}