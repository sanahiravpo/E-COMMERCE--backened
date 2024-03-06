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
        public async Task<IActionResult>  AddToWishList( int productid)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwtToken = splitToken[1];
                var isExist = await _wishlist.AddToWishList(jwtToken, productid);
                if (!isExist)
                {
                    return BadRequest("item already in the whishList");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task <IActionResult> GetAllWishlistDetails()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwtToken = splitToken[1];
               

                return Ok(await _wishlist.GetAllWishlistDetails(jwtToken));
            }
            catch(Exception ex)
            {
               throw new Exception(ex.Message);
            }
        }
        [HttpDelete]
        [Authorize]
        public IActionResult DeleteWishList( int productid)
        {

            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var splitToken = token.Split(' ');
            var jwtToken = splitToken[1];
            _wishlist.DeleteWishList(jwtToken, productid);
            return Ok();
        }
    }
}
