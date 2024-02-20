using E_COMMERCE_WEBSITE.Models;
using E_COMMERCE_WEBSITE.Models.DTO;
using E_COMMERCE_WEBSITE.Repositories.categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_COMMERCE_WEBSITE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _category;
        private readonly IConfiguration _configuration;
        public CategoryController(ICategory category, IConfiguration configuration)
        {
            _category = category;
            _configuration = configuration;
        }
        [HttpPost("addcategory")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult > AddCategory(CategoryDTO categorydto)
        {
            try
            {
                await _category.AddCategory(categorydto);
                return Ok(categorydto);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                return Ok(await _category.GetAllCategories());
            }
            catch
            {
                return NotFound();
            }
            
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                return Ok(await _category.GetCategoryById(id));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateCategory(CategoryDTO updatecateg, int id)
        {
            try
            {
                await _category.UpdateCategory(updatecateg, id);
                return Ok(updatecateg);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _category.DeleteCategory(id);
                return Ok("category is successfully deleted");
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}

