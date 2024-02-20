using E_COMMERCE_WEBSITE.Models;
using E_COMMERCE_WEBSITE.Models.DTO;

namespace E_COMMERCE_WEBSITE.Repositories.categories
{
    public interface ICategory
    {
        Task AddCategory(CategoryDTO categorydto);
       Task<List<Category>> GetAllCategories();
        Task<CategoryDTO> GetCategoryById(int id);
        Task UpdateCategory(CategoryDTO updatecateg, int id);
        Task DeleteCategory(int id);
    }
}
