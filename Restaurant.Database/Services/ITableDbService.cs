using System.Linq;
using Restaurant.Database.Models;

namespace Restaurant.Database.Services
{
    public interface ITableDbService
    {
        Table CreateTable(int number, int seats);

        void UpdateTable(Table table);

        void DeleteTable(Table table);

        IQueryable<Table> GetAllTables();
    }
}
