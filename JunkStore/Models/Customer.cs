using System.ComponentModel.DataAnnotations;

namespace JunkStore.Models
{
    public class Customer
    {
        [Key]
        public Guid CustomerId { get; set; }

        [Display(Name = "First name")]
        [Required]
        public string FirstName { get; set; }

        [Required]
        [Display(Name ="Last name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Display(Name = "Phone number")]
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();

        public string UserId;

        public ApplicationUser UserIdentity;
    }
}

