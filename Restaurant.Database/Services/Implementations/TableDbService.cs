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

        public IQueryable<Table> GetAllTables()
        {
            return context.Tables;
        }

        #endregion
    }
}
