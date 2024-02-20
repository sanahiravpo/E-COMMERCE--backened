using E_COMMERCE_WEBSITE.Models.DTO;

namespace E_COMMERCE_WEBSITE.Repositories.WishlistRepository
{
    public interface IWishlist
    {
        public Task AddToWishList(int userid, int productid);
        public Task<List<WishlistDTO>> GetAllWishlistDetails(int userid);
        public Task DeleteWishList(int userid,int productid);
    }
}
