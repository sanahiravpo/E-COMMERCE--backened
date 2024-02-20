using E_COMMERCE_WEBSITE.Models;
using E_COMMERCE_WEBSITE.Models.DTO;
using E_COMMERCE_WEBSITE.Repositories.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_COMMERCE_WEBSITE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _product;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IProduct product, IWebHostEnvironment webHostEnvironment)
        {
            _product = product;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                return Ok(await _product.GetAllProducts());
            }
            catch
            {
                return BadRequest("products not found");
            }
        }
        [HttpGet("{id}")]
       
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                return Ok(await _product.GetProductById(id));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddProduct([FromBody] ProductClientDTO productsto, IFormFile image)
        {
            try
            {
               await _product.AddProduct(productsto,image);
                return Ok("product added successfully");
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductClientDTO updateproducts, int id,IFormFile image)
        {
            try
            {
               await _product.UpdateProduct(updateproducts,id,image)
;
                return Ok("product updated successfully");
            }
            catch
            {
                return BadRequest();    
            }
           

        }
        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {

           await _product.DeleteProduct(id);
            return Ok("product deleted successfully");
        }
        
        [HttpGet("Category")]
        public async Task<IActionResult> GetAllProductsByCategory(int id)
        {
            try
            {
                return Ok(await _product.GetAllProductsByCategory(id));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("Totalproductspurchased")]
        public async Task<IActionResult> GetTotalproductspurchased(int userid)

        {
            try
            {
                var product = await _product.GetTotalproductspurchased(userid);
                return Ok(product);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
