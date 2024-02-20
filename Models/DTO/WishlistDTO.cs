namespace E_COMMERCE_WEBSITE.Models.DTO
{
    public class WishlistDTO
    {
        
        public int Id { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }
        public decimal UnitPrice { get; set; }
        public string productImage {  get; set; }
    }
}
