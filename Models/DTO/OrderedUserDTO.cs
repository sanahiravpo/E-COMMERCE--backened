namespace E_COMMERCE_WEBSITE.Models.DTO
{
    public class OrderedUserDTO
    {

    public int Id { get; set; }
       

      
        public string customername { get; set; }

        public string customeremail { get; set; }
        public string customerphone { get; set; }
        public string customercity { get; set; }
        public string Homeaddress { get; set; }
        public string TransactionId { get; set; }
        public string OrderStatus { get; set; }
    }
}
