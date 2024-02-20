using System.ComponentModel.DataAnnotations;

namespace E_COMMERCE_WEBSITE.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string productName {get; set; }
     
        public string productDescription { get; set; }
    
        public string productImage { get; set; }
        public Decimal UnitPrice { get; set; }
        public int categid { get; set; }
 
 
        //public virtual List<Order> order { get; set; }
        public virtual List<CartItem> cartitem { get; set; }
        public virtual Category categ { get; set; }
      
    }
}
