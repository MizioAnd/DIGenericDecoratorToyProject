namespace ProductServices.Services
{
    public class AdjustInventoryService : ICommandService
    {
        private readonly IInventoryRepository _repository;

        public AdjustInventoryService(IInventoryRepository repository)
        {
            // A non-used dummy field
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Execute(object cmd)
        {
            var command = (AdjustInventory)cmd;

            Guid id = command.ProductId;
            bool decrease = command.Decrease;
            int quantity = command.Quantity;
        }

        public void Execute(AdjustInventory command)
        {
            var ProductId = command.ProductId;
        }
    }
}