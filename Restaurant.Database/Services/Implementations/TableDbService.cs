using System;
using System.Linq;
using Restaurant.Database.Data;
using Restaurant.Database.Models;

namespace Restaurant.Database.Services.Implementations
{
    public class TableDbService : ITableDbService
    {
        #region Declarations

        private readonly RestaurantDbContext context;

        #endregion

        #region Constructors

        public TableDbService()
        {
            context = new RestaurantDbContext();
        }

        #endregion

        #region Methods

        public Table CreateTable(int number, int seats)
        {
            Table table = new Table()
            {
                Number = number,
                Seats = seats
            };

            context.Tables.Add(table);

            context.SaveChanges();

            return table;
        }

        public void UpdateTable(Table table)
        {
            Table entityTable = context.Tables.FirstOrDefault(t => t.Id == table.Id);

            if (entityTable == null)
                throw new Exception();

            entityTable.Number = table.Number;
            entityTable.Seats = table.Seats;
            entityTable.IsTaken = table.IsTaken;

            context.SaveChanges();
        }

        public void DeleteTable(Table table)
        {
            context.Tables.Remove(table);

            context.SaveChanges();
        }

        public IQueryable<Table> GetAllTables()
        {
            return context.Tables;
        }

        #endregion
    }
}
