using JunkStore.Models.Types;
using System.ComponentModel.DataAnnotations;

namespace JunkStore.Models
{
    public class Shipping
    {
        [Key]
        public Guid ShippingId { get; set; }

        [Required]
        public Guid OrderId { get; set; }
        [Required]
        public string ShippingMethod { get; set; }

        public string TrackingNumber { get; set; }

        public DateTime ShippingDate { get; set; }
        [Required]
        public ShippingStatus Status { get; set; }

        public Order Order { get; set; }
    }
}
