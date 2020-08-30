using Restaurant.Database.Models;
using System.Collections.Generic;

namespace Restaurant.Services
{
    public interface ITableService
    {
        Table CreateTable(int number, int seats);

        void UpdateTable(Table table);

        void DeleteTable(Table table);

        IEnumerable<Table> GetAllTables();
    }
}
