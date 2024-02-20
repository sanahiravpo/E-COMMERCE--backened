using E_COMMERCE_WEBSITE.Models;
using E_COMMERCE_WEBSITE.Models.DTO;
using E_COMMERCE_WEBSITE.Repositories.OrderRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_COMMERCE_WEBSITE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrder _order;



        public OrderController(IOrder order)
        {
            _order=order;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrder(int userid, int productid, int quantity)
        {
            try
            {
                await _order.CreateOrder(userid, productid, quantity);
                return Ok("you successfully make an order");
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllOrders(int userid)
        {
            try
            {
                return Ok(await _order.GetAllOrders(userid));
            }
            catch
            {
                return NotFound();
            }
          
        }
        [HttpGet("total-revenue")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> TotalRevenue()
        {
            try
            {
                return Ok(await _order.TotalRevenue());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("get-all-user-order-details")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAlluserOrders(int userid)
        {
            try
            {
                return Ok(await _order.GetAlluserOrders(userid));
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
