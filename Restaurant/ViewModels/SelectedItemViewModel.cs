using Restaurant.Database.Models;
using System.Collections.Generic;

namespace Restaurant.ViewModels
{
    public class SelectedItemViewModel : BaseViewModel
    {
        #region Declarations

        private List<Item> items;

        #endregion

        #region Properties

        public List<Item> Items
        {
            get
            {
                if (items == null)
                    items = new List<Item>();

                return items;
            }
        }

        public int Count { get; set; } = 1;

        public decimal Total { get; set; }

        #endregion
    }
}
