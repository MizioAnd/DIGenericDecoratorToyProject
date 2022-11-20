using System.Transactions;

namespace ProductServices.Services;

public class TransactionCommandServiceDecorator : ICommandService
{
    private readonly ICommandService _inner;

    public TransactionCommandServiceDecorator(ICommandService inner)
    {
        _inner = inner;
    }

    public void Execute(AdjustInventory command)
    {
        using (var scope = new TransactionScope())
        {
            _inner.Execute(command);

            scope.Complete();
        }
    }
}
