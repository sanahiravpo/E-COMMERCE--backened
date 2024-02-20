using AutoMapper;
using E_COMMERCE_WEBSITE.Context;
using E_COMMERCE_WEBSITE.Models;
using E_COMMERCE_WEBSITE.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace E_COMMERCE_WEBSITE.Repositories.categories
{
    public class CategoryRepository : ICategory
    {
        private readonly UserDBContext _dbContext;
        private readonly IMapper _mapper;
        public CategoryRepository(UserDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task AddCategory(CategoryDTO categorydto)
        {
            var Addcateg = _mapper.Map<Category>(categorydto);
            _dbContext.categories.Add(Addcateg);
           await  _dbContext.SaveChangesAsync();
        }
        public async Task<List<Category>> GetAllCategories()
        {
            var categ =  _dbContext.categories.ToList();
            return categ;
        }
        public async Task<CategoryDTO> GetCategoryById(int id)
        {
            var findcategory = await _dbContext.categories.FirstOrDefaultAsync(e => e.Id == id);

            return _mapper.Map<CategoryDTO>(findcategory);
        }
        public async Task UpdateCategory(CategoryDTO updatecateg, int id)
        {
            var findcategory = _dbContext.categories.FirstOrDefault(e => e.Id == id);
            if (findcategory != null)
            {
                findcategory.name = updatecateg.name;

               await _dbContext.SaveChangesAsync();
            }
        }
        public async Task DeleteCategory(int id)
        {
            var finded =await  _dbContext.categories.FirstOrDefaultAsync(a => a.Id == id);
            _dbContext.categories.Remove(finded);
            await _dbContext.SaveChangesAsync();

        }
    }
}
