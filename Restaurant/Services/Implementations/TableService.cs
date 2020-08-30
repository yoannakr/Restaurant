using System.Collections.Generic;
using Restaurant.Database.Models;
using Restaurant.Database.Services;
using Restaurant.Database.Services.Implementations;

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

        public Table CreateTable(int number, int seats)
        {
            Table table = tableDb.CreateTable(number, seats);

            return table;
        }

        public void UpdateTable(Table table)
        {
            tableDb.UpdateTable(table);
        }

        public void DeleteTable(Table table)
        {
            tableDb.DeleteTable(table);
        }

        public IEnumerable<Table> GetAllTables()
        {
            return tableDb.GetAllTables();
        }

        #endregion
    }
}
