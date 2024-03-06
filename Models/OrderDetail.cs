using System.ComponentModel.DataAnnotations;

namespace E_COMMERCE_WEBSITE.Models
{
    public class OrderDetail
    {

        public int Id { get; set; }

        public int orderid { get; set; }
        [Required]
        public int Productid { get; set; }
        public Decimal Totalprice {  get; set; }
        public int Quantity {  get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
