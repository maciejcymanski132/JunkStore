namespace JunkStore.Models
{
    public class OrderItem
    {
        public Guid OrderItemId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public ProductInventory ProductInventory { get; set; }
        public Order Order { get; set; }
    }
}
