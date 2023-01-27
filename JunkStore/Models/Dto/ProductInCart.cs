namespace JunkStore.Models.Dto
{
    public class ProductInCart : IProduct
    {
        public string Name { get; set; }

        public Guid ProductId { get; set; }

        public ProductType ProductType { get; set; }

        public int Quantity { get; set; }
    }
}
