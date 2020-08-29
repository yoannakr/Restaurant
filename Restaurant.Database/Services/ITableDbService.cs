using System.Linq;
using Restaurant.Database.Models;

namespace Restaurant.Database.Services
{
    public interface ITableDbService
    {
        Table CreateTable(int number, int seats);

        IQueryable<Table> GetAllTables();
    }
}
