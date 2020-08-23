using System.Linq;
using Restaurant.Database.Models;

namespace Restaurant.Database.Services
{
    public interface ITableDbService
    {
        IQueryable<Table> GetAllTables();
    }
}
