using E_COMMERCE_WEBSITE.Models;
using E_COMMERCE_WEBSITE.Models.DTO;

namespace E_COMMERCE_WEBSITE.Repositories.CartServices
{
    public interface ICart
    {
        Task AddProductToCart(string token, int productid);
        public Task<List<CartDTO>> Getcartdetail(string token);
        public Task Deletecartitem(string token, int productid);
        public Task QuantityIncrease(string token, int productid);
        public Task QuantityDecrease(string token, int productid);




    }
}
