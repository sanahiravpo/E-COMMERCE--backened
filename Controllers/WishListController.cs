using E_COMMERCE_WEBSITE.Models.DTO;
using E_COMMERCE_WEBSITE.Repositories.WishlistRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_COMMERCE_WEBSITE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {


        private readonly IWishlist _wishlist;
        public WishListController(IWishlist wishlist)
        {
            _wishlist = wishlist;
        }

        [HttpPost]
        [Authorize]
        public IActionResult  AddToWishList(int userid, int productid)
        {
            _wishlist.AddToWishList(userid, productid);
            return Ok();
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllWishlistDetails(int userid)
        {
           
            return Ok(_wishlist.GetAllWishlistDetails(userid));
        }
        [HttpDelete]
        [Authorize]
        public IActionResult DeleteWishList(int userid,int productid)
        {
            _wishlist.DeleteWishList(userid, productid);
            return Ok();
        }
    }
}
