using System.ComponentModel.DataAnnotations;

namespace JunkStore.Models.Dto
{
    public class ShoppingCart
    {
        [Key]
        public Guid CustomerId { get; set; }
        [Required]
        public Customer Customer { get; set; }
        [Required]
        public ICollection<ProductInCart> ProductsInCart { get; set;} = new List<ProductInCart>();
    }
}
