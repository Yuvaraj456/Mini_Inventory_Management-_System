using System.ComponentModel.DataAnnotations;

namespace Mini_Inventory_Management_System.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Please Select Product")]
        public int ProductId { get; set; }
        [Required(ErrorMessage ="Please Enter Quantity")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity should be Greater than 0")]
        public int Quantity { get; set; }
        public Product Product { get; set; }
    }
}
