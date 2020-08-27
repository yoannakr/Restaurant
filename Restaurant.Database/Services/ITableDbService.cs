using System.Linq;
using Restaurant.Database.Models;

namespace Restaurant.Database.Services
{
    public interface ITableDbService
    {
        void CreateTable(long number, int seats);

        IQueryable<Table> GetAllTables();
    }
}
