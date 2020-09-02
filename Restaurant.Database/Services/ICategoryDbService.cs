using Restaurant.Database.Models;
using System.Linq;

namespace Restaurant.Database.Services
{
    public interface ICategoryDbService
    {
        IQueryable<Category> GetAllCategories();
    }
}
