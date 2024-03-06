using System.ComponentModel.DataAnnotations;

namespace E_COMMERCE_WEBSITE.Models
{
    public class User
    {
        public int id {  get; set; }
      
        public string username { get; set; }
        [Required]
        [MinLength(8)]
        public string passwordHash { get; set; }
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string phone { get; set; }
        public string Role {  get; set; }
      
        public virtual List<WishList> WishLists { get; set; }
        public  virtual  Cart carts {  get; set; }  
    public  virtual List<Order>  orders { get; set; }
       
    }
}
