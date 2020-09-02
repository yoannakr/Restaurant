using System.Linq;
using Restaurant.Database.Data;
using Restaurant.Database.Models;

namespace Restaurant.Database.Services.Implementations
{
    public class CategoryDbService : ICategoryDbService
    {
        #region Declarations

        private readonly RestaurantDbContext context;

        #endregion

        #region Constructors

        public CategoryDbService()
        {
            context = new RestaurantDbContext();
        }

        #endregion

        #region Methods

        public IQueryable<Category> GetAllCategories()
        {
            return context.Categories;
        }

        #endregion
    }
}
