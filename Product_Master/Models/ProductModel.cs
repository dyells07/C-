using System.ComponentModel.DataAnnotations;

namespace Product_Master.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(50, ErrorMessage = "Product name cannot exceed 50 characters.")]
        public string ProductName { get; set; }

        [StringLength(500, ErrorMessage = "Product description cannot exceed 500 characters.")]
        public string ProductDescription { get; set; }

        [Required(ErrorMessage = "Product cost is required.")]
        [Range(0, 100000, ErrorMessage = "Product cost must be between 0 and 100000.")]
        public decimal ProductCost { get; set; }

        [Required(ErrorMessage = "Stock is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock must be a positive integer.")]
        public int Stock { get; set; }
    }
}