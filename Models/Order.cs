using System.ComponentModel.DataAnnotations;

namespace E_COMMERCE_WEBSITE.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]

        public int Userid { get; set;}
        [Required]
        public int Productid { get; set; }
        [Required]
        public int Quantity {  get; set; }
       
        public virtual Product Products { get; set; }
        public virtual User User { get; set; }

    }
}
