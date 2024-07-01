using System.ComponentModel.DataAnnotations;

namespace WebApplication17.Models.Products
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public bool ProductAvailability { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public double ProductPrice { get; set; }
        public byte[]? Photo { get; set; }
    }
}
