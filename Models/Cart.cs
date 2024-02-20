using System.ComponentModel.DataAnnotations;

namespace E_COMMERCE_WEBSITE.Models
{
    public class Cart
    {
        public int id { get; set; }

       
        public int userid { get; set; }

     
       
        public virtual List<CartItem> cartItems { get; set; }
     
            public virtual User user { get; set; }




    }
}
