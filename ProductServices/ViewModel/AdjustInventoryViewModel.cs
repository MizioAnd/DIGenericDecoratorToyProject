using ProductServices.Services;

namespace ProductServices.ViewModel;

public class AdjustInventoryViewModel
{
    public AdjustInventory Command { get; internal set; }
    
    public AdjustInventoryViewModel(AdjustInventory adjustInventory)
    {
        Command = adjustInventory;
    }
}