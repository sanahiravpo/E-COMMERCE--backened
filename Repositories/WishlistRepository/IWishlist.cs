using E_COMMERCE_WEBSITE.Models.DTO;

namespace E_COMMERCE_WEBSITE.Repositories.WishlistRepository
{
    public interface IWishlist
    {
        public Task<bool> AddToWishList(string token, int productid);
        public Task<List<WishlistDTO>> GetAllWishlistDetails(string token);
        public Task DeleteWishList(string token, int productid);
    }
}
