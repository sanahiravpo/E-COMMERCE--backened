namespace E_COMMERCE_WEBSITE.Models.DTO
{
    public class ProductClientDTO
    {

        public int productId { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }
        public decimal UnitPrice { get; set; }
        public string productImage { get; set; }
        public string category { get; set; }
    }
}
