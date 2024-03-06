using E_COMMERCE_WEBSITE.Models;
using E_COMMERCE_WEBSITE.Models.DTO;

namespace E_COMMERCE_WEBSITE.Repositories.ProductService
{
    public interface IProduct
    {

        Task AddProduct(ProductDTO productsto, IFormFile image);
        Task UpdateProduct(ProductDTO updateproducts, int id, IFormFile image);
        Task DeleteProduct(int id); 
       Task<List<ProductClientDTO>> GetAllProducts();
        Task<ProductClientDTO> GetProductById(int id);
       Task< List<ProductClientDTO>> GetAllProductsByCategory(int id);
        public Task<List<ProductClientDTO> >GetTotalproductspurchased(string token);

        public  Task<List<ProductClientDTO>> Searcheditems(string search);



    }
}
