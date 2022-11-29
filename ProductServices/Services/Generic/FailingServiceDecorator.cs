namespace ProductServices.Services.Generic;

public class FailingServiceDecorator<TCommand> : ICommandService<TCommand>
{
    private readonly IInventoryRepository _repository;
    private readonly ICommandService<TCommand> _inner;

    private int _counter = 0;

    public FailingServiceDecorator(IInventoryRepository repository, ICommandService<TCommand> inner)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _inner = inner ?? throw new ArgumentNullException(nameof(inner));
    }

    public void Execute(TCommand command)
    {
        FailEveryThirdCallToMehtod();
        _inner.Execute(command);
    }

    private void FailEveryThirdCallToMehtod()
    {
        _counter ++;
        if (_counter == 3)
        {
            _counter = 0;
            throw new BadHttpRequestException("Fails on every third call to method");
        }
    }
}
