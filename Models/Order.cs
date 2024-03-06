using System.ComponentModel.DataAnnotations;

namespace E_COMMERCE_WEBSITE.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public int Userid { get; set;}

        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerEmail { get; set; }
        [Required]
        public string CustomerPhone { get; set; }
        [Required]
        public string CustomerCity { get; set; }
        [Required]
        public string HomeAddress { get; set; }
     
        public string OrderStatus { get; set; }
        
        public string TransactionId { get; set; }

      public List<OrderDetail> orderdetail { get; set; }
        public User User { get; set; }

    }
}
