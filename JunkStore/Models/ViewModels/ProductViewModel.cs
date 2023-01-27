namespace JunkStore.Models.ViewModels
{
    public class ProductViewModel
    {
        public IEnumerable<Product> Products { get; set; }

        public IEnumerable<ProductType> ProductTypes { get; set; }
    }
}
