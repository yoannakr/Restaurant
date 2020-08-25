using System.Linq;
using Restaurant.Services;
using System.Collections.Generic;
using Restaurant.Services.Implementations;

namespace Restaurant.ViewModels
{
    public class AllTablesViewModel : BaseViewModel
    {
        #region Declarations

        private List<TableViewModel> tables;
        private readonly ITableService tableService;

        #endregion

        #region Constructors

        public AllTablesViewModel(MenuViewModel menuViewModel)
        {
            tableService = new TableService();
            MenuViewModel = menuViewModel;
        }

        #endregion

        #region Properties

        public List<TableViewModel> Tables
        {
            get
            {
                if (tables == null)
                {
                    tables = new List<TableViewModel>();

                    tables = tableService.GetAllTables().Select(t => new TableViewModel(MenuViewModel)
                    {
                        Table = t,
                        IsTaken = t.IsTaken
                    }).ToList();
                }

                return tables;
            }
        }

        public MenuViewModel MenuViewModel { get; set; }

        #endregion


    }
}
