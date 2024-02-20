using System.ComponentModel.DataAnnotations;

namespace E_COMMERCE_WEBSITE.Models
{
    public class CartItem
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int cartid { get; set; }
        [Required]
        public int ProdId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public Decimal price { get; set; }
        [Required]
        public Decimal Totalprice { get; set; }
       
        public virtual Product products { get; set; }
        public virtual Cart cartss { get; set; }

     


    }
}
