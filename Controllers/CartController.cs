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
        [Authorize]
        public async Task<IActionResult> AddProductToCart( int productid) {
            
                   var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                    var splitToken = token.Split(' ');
                   var jwttoken= splitToken[1];
                await _cart.AddProductToCart(jwttoken, productid);
                return Ok("product successfully added to cart");
           
          
        }
        [HttpGet]
        [Authorize]

        public async Task<ActionResult> Getcartdetail()
        {
            
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwttoken = splitToken[1];
                return Ok(await _cart.Getcartdetail(jwttoken));
       
        }
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Deletecartitem( int productid)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwttoken = splitToken[1];
                await _cart.Deletecartitem(jwttoken, productid);
                return Ok("the item is removed from the cart");
            }
            catch
            {
                return BadRequest();
            } 
        }


        [HttpPut("increase-quantity")]
        [Authorize]
        public async Task<IActionResult> QuantityIncrease( int productid)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwttoken = splitToken[1];
              await  _cart.QuantityIncrease(jwttoken, productid);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }
        [HttpPut("decrease-quantity")]
        [Authorize]
        public async Task<IActionResult> QuantityDecrease( int productid)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwttoken = splitToken[1];
                await _cart.QuantityDecrease(jwttoken, productid);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
