using E_COMMERCE_WEBSITE.Models;
using E_COMMERCE_WEBSITE.Models.DTO;

namespace E_COMMERCE_WEBSITE.Repositories.CartServices
{
    public interface ICart
    {
        Task AddProductToCart(int userid, int productid);
        public Task<List<CartDTO>> Getcartdetail(int UserId);
        public Task Deletecartitem(int UserId, int productid);
        public Task QuantityIncrease(int userid, int productid);
        public Task QuantityDecrease(int UserId, int productid);




    }
}
