namespace ProductServices.Services.Generic;

public class AdjustInventoryFailingService : ICommandService<AdjustInventory>
{
    private readonly IInventoryRepository _repository;

    public int Counter { get; set; } = 0;

    public AdjustInventoryFailingService(IInventoryRepository repository)
    {
        _repository = repository;
    }

    public void Execute(AdjustInventory command)
    {
        FailEveryThirdCallToMehtod();
        var ProductId = command.ProductId;
    }

    private void FailEveryThirdCallToMehtod()
    {
        Counter ++;
        if (Counter == 3)
        {
            Counter = 0;
            throw new BadHttpRequestException("Fails on every third call to method");
        }
    }
}
