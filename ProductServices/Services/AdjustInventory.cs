namespace ProductServices.Services;

public class AdjustInventory
{
    public Guid ProductId { get; internal set; } = new Guid();
    public bool Decrease { get; internal set; }
    public int Quantity { get; internal set; }
}
