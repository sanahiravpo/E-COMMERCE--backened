using E_COMMERCE_WEBSITE.Models;
using E_COMMERCE_WEBSITE.Models.DTO;

namespace E_COMMERCE_WEBSITE.Repositories.ProductService
{
    public interface IProduct
    {

        Task AddProduct(ProductClientDTO productsto, IFormFile image);
        Task UpdateProduct(ProductClientDTO updateproducts, int id, IFormFile image);
        Task DeleteProduct(int id); 
       Task<List<ProductClientDTO>> GetAllProducts();
        Task<ProductClientDTO> GetProductById(int id);
       Task< List<ProductClientDTO>> GetAllProductsByCategory(int id);
        public Task<List<ProductClientDTO> >GetTotalproductspurchased(int userid);



    }
}
