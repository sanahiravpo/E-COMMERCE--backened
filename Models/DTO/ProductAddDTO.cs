namespace E_COMMERCE_WEBSITE.Models.DTO
{
    public class ProductAddDTO
    {

        public int Id { get; set; }
        public string productName { get; set; }

        public decimal UnitPrice { get; set; }
        public string productDescription { get; set; }

        public int categid { get; set; }
    }
}
