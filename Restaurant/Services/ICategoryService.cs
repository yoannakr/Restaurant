using System.Linq;
using Restaurant.Services.Models.Category;

namespace Restaurant.Services
{
    public interface ICategoryService
    {
        IQueryable<CategoryDto> GetAllCategories();
    }
}
