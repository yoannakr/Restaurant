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

        public void CreateTable(long number, int seats)
        {
            tableDb.CreateTable(number, seats);
        }

        public IEnumerable<Table> GetAllTables()
        {
            return tableDb.GetAllTables();
        }

        #endregion
    }
}
