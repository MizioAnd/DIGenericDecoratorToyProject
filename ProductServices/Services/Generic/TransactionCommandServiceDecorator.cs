using System.Transactions;

namespace ProductServices.Services.Generic;

public class TransactionCommandServiceDecorator<TCommand> : ICommandService<TCommand>
{
    private readonly ICommandService<TCommand> _inner;

    public TransactionCommandServiceDecorator(ICommandService<TCommand> inner)
    {
        _inner = inner;
    }

    public void Execute(TCommand command)
    {
        using (var scope = new TransactionScope())
        {
            _inner.Execute(command);

            scope.Complete();
        }
    }
}
