using E_COMMERCE_WEBSITE.Models;
using E_COMMERCE_WEBSITE.Models.DTO;
using E_COMMERCE_WEBSITE.Repositories.OrderRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        public async Task<IActionResult> CreateOrder(OrderDTO orderdto)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwttoken = splitToken[1];
                await _order.CreateOrder(jwttoken, orderdto);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwttoken = splitToken[1];
                return Ok(await _order.GetAllOrders(jwttoken));
            }
            catch
            {
                return NotFound();
            }
          
        }
        //[HttpGet("total-revenue")]
        //[Authorize(Roles = "admin")]
        //public async Task<IActionResult> TotalRevenue()
        //{
        //    try
        //    {
        //        return Ok(await _order.TotalRevenue());
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}

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
        [HttpGet("get-user-orders-admin")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetuserordersAdmin(int userid)
        {
            try
            {

                return Ok(await _order.GetuserordersAdmin(userid));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("get-user-info-admin")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetUserDetails(int userid)
        {
            try
            {

                return Ok(await _order.GetUserDetails(userid));
            }
            catch
            {
                return NotFound();
            }
        }

    }
}
