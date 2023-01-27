namespace JunkStore.Models
{
    public class ProductInventory
    {
        public Guid ProductInventoryId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

    }
}
