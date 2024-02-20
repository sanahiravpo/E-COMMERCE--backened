using E_COMMERCE_WEBSITE.Models.DTO;
using E_COMMERCE_WEBSITE.Repositories.CartServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_COMMERCE_WEBSITE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICart _cart;
        public CartController(ICart cart) {
            _cart=cart;
        }
        [HttpPost("addproduct")]

        public async Task<IActionResult> AddProductToCart([FromQuery] int userid, int productid) {
            try
            {
                await _cart.AddProductToCart(userid, productid);
                return Ok("product successfully added to cart");
            }
            catch (Exception ex) { }
            {
                return BadRequest();
            }
          
        }
        [HttpGet]
      

        public async Task<IActionResult> Getcartdetail(int UserId)
        {
            
                return Ok(await _cart.Getcartdetail(UserId));
            
            
        }
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Deletecartitem(int UserId, int productid)
        {
            try
            {
                await _cart.Deletecartitem(UserId, productid);
                return Ok("the item is removed from the cart");
            }
            catch
            {
                return BadRequest();
            } 
        }


        [HttpPut("increase-quantity")]
        [Authorize]
        public async Task<IActionResult> QuantityIncrease(int userid, int productid)
        {
            try
            {
                _cart.QuantityIncrease(userid, productid);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
           
        }
        [HttpPut("decrease-quantity")]
        [Authorize]
        public async Task<IActionResult> QuantityDecrease(int UserId, int productid)
        {
            try
            {
                await _cart.QuantityDecrease(UserId, productid);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
