using System.Linq;
using Restaurant.Database.Services;
using Restaurant.Services.Models.Category;
using Restaurant.Database.Services.Implementations;

namespace Restaurant.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        #region Declarations

        private readonly ICategoryDbService categoryDb;

        #endregion

        #region Constructors

        public CategoryService()
        {
            categoryDb = new CategoryDbService();
        }

        #endregion

        #region Methods

        public IQueryable<CategoryDto> GetAllCategories()
        {
            return categoryDb.GetAllCategories().Select(c => new CategoryDto()
            {
                Id = c.Id,
                Name = c.Name
            });
        }

        #endregion
    }
}
