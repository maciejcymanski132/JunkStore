using JunkStore.Models.Types;
using System.ComponentModel.DataAnnotations;

namespace JunkStore.Models
{
    public class Payment
    {
        [Key]
        public Guid PaymentId { get; set; }

        [Required]
        public Guid OrderId { get; set; }

        public DateTime PaymentDate { get; set; }

        public PaymentStatus Status { get; set; }

        public Order Order { get; set; }
    }
}
