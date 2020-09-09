using System.Collections.Generic;
using System.Linq;
using Restaurant.Database.Models;
using Restaurant.Database.Services;
using Restaurant.Database.Services.Implementations;
using Restaurant.Services.Models.Table;

namespace Restaurant.Services.Implementations
{
    public class TableService : ITableService
    {
        #region Declarations

        private readonly ITableDbService tableDb;

        #endregion

        #region Constructors

        public TableService()
        {
            tableDb = new TableDbService();
        }

        #endregion

        #region Methods

        public TableDto CreateTable(int number, int seats)
        {
            Table table = tableDb.CreateTable(number, seats);

            TableDto tableDto = new TableDto()
            {
                Id = table.Id,
                Number = table.Number,
                Seats = table.Seats
            };

            return tableDto;
        }

        public void UpdateTable(Table table)
        {
            tableDb.UpdateTable(table);
        }

        public void DeleteTable(Table table)
        {
            tableDb.DeleteTable(table);
        }

        public IEnumerable<TableDto> GetAllTables()
        {
            return tableDb.GetAllTables().Select(t=>new TableDto()
            {
                Id = t.Id,
                Number = t.Number,
                Seats = t.Seats
            });
        }

        #endregion
    }
}
