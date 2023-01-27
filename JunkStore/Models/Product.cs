using System.ComponentModel.DataAnnotations;

namespace JunkStore.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }

        public byte[] Image { get; set; }

        public string ProductTypeId { get; set; }

        public ProductType ProductType { get; set; }
    }

}


