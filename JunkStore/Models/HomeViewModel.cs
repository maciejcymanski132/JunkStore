namespace JunkStore.Models
{
    public class HomeViewModel
    {
        public ICollection<Product> Products { get; set; }

        public ICollection<ProductType> ProductTypes { get; set; }
    }
}
