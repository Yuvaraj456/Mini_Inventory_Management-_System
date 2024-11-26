using System.ComponentModel.DataAnnotations;

namespace Mini_Inventory_Management_System.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1.00, double.MaxValue, ErrorMessage = "Price Should be Greater than 0")]
        public double Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stock Should not be in negative")]
        public int Stock { get; set; }
    }
}
