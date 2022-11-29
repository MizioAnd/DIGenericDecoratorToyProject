namespace ProductServices.Services.Generic;

public class AdjustInventoryService : ICommandService<AdjustInventory>
{
    private readonly IInventoryRepository _repository;

    public AdjustInventoryService(IInventoryRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public void Execute(AdjustInventory command)
    {
        var ProductId = command.ProductId;
    }
}
