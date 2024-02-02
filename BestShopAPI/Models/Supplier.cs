using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BestShopAPI.Models
{
    [Table("Suppliers")]
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }
        [Required]
        public string? Name { get; set; }

    }
}
