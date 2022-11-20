namespace ProductServices.Services
{
    public interface ICommandService
    {
        void Execute(AdjustInventory command);
    }
}