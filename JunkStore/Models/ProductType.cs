using System.ComponentModel.DataAnnotations;

namespace JunkStore.Models
{
    public class ProductType
    {
        [Key]
        public Guid ProductTypeId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
