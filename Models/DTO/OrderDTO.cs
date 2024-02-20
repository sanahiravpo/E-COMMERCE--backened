namespace E_COMMERCE_WEBSITE.Models.DTO
{
    public class OrderDTO
    {

        public int Id { get; set; }
        public int userid {  get; set; }
        public string productImage { get; set; }
        public string productname { get; set; }
        public string productdescription { get; set; }
        public Decimal productprice { get; set; }
        public int Quantity { get; set; }
        public Decimal TotalPrice { get; set; }
        public string email { get; set; }
      
        public string phone { get; set; }

    }
}
