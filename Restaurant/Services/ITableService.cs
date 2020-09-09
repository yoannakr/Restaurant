using Restaurant.Database.Models;
using Restaurant.Services.Models.Table;
using System.Collections.Generic;

namespace Restaurant.Services
{
    public interface ITableService
    {
        TableDto CreateTable(int number, int seats);

        void UpdateTable(Table table);

        void DeleteTable(Table table);

        IEnumerable<TableDto> GetAllTables();
    }
}
