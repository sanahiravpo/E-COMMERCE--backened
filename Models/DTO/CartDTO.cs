namespace E_COMMERCE_WEBSITE.Models.DTO
{
    public class CartDTO
    {

        public int Id { get; set; }

        public int ProdId { get; set; }
        public string productImage { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }
        public Decimal price { get; set; }

        public int Quantity { get; set; }
        public decimal Totalprice { get; set; }


    }
}
