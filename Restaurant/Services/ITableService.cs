using Restaurant.Database.Models;
using System.Collections.Generic;

namespace Restaurant.Services
{
    public interface ITableService
    {
        void CreateTable(long number, int seats);

        IEnumerable<Table> GetAllTables();
    }
}
