using System.Collections.Generic;
using Restaurant.Common.InstanceHolder;
using Restaurant.Database.Models;
using Restaurant.Database.Services;
using Restaurant.Database.Services.Implementations;
using Restaurant.ViewModels;

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

            CollectionInstance.Instance.Tables.Add(new TableViewModel()
            {
                Table = table,
                IsTaken = table.IsTaken
            });

            return table;
        }

        public IEnumerable<Table> GetAllTables()
        {
            return tableDb.GetAllTables();
        }

        #endregion
    }
}
