using JunkStore.Models.Types;
using System.ComponentModel.DataAnnotations;

namespace JunkStore.Models
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }

        [Required]
        public string CustomerId { get; set; }

        [Display(Name = "Created date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Status")]
        [Compare("OrderStatus", ErrorMessage = "Order status must be completed when order total is greater than 0.")]
        public OrderStatus OrderStatus { get; set; }

        [Display(Name = "Total")]
        [GreaterThanZero(ErrorMessage = "Order total must be greater than 0 when order status is completed.")]
        public decimal OrderTotal { get; set; }

        public ICollection<Product> Products { get; set; }

        public Payment Payment { get; set; }

        public Shipping Shipping { get; set; }
    }
}
