using System.ComponentModel.DataAnnotations;

namespace E_COMMERCE_WEBSITE.Models
{
    public class WishList
    {
        public int Id { get; set; }
        [Required]
        public int userid {  get; set; }
        [Required]
        public int productid {  get; set; }
        public virtual Product products { get; set;}

        public virtual User Users { get; set; }

    }
}
