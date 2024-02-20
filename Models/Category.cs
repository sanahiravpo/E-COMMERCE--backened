using System.ComponentModel.DataAnnotations;

namespace E_COMMERCE_WEBSITE.Models
{
    public class Category
    {
        public int Id { get; set; }
     
       public string name {  get; set; }

        public virtual List <Product> product { get; set; }

    }
}
