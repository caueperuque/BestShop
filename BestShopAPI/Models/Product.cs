using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BestShopAPI.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Nome do produto tem que ter menos de 50 caracteres")]
        public string? Name { get; set; }
        [StringLength(250)]
        public string? Description { get; set; }
        [Required]
        [Range(1, 100000, ErrorMessage = "O valor do produto tem que estar entre 1 a 100000")]
        public decimal Price { get; set; }
        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }

        public int SupplierId { get; set; }
    }
}
