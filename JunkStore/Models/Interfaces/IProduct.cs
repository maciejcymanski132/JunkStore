
namespace JunkStore.Models
{
    public interface IProduct
    {
        string Name { get; set; }
        Guid ProductId { get; set; }
        ProductType ProductType { get; set; }
    }
}