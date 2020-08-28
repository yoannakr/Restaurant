using System.Linq;
using Restaurant.Services;
using System.Collections.ObjectModel;
using Restaurant.Services.Models.Item;
using Restaurant.Services.Implementations;

namespace Restaurant.Common.AllCollections
{
    public class CollectionHolder
    {
        #region Declarations

        private readonly IItemService itemService;
        private static CollectionHolder instance = null;
        private ObservableCollection<ItemDto> items;

        #endregion

        #region Constructors

        private CollectionHolder()
        {
            itemService = new ItemService();
        }

        #endregion

        #region Properties

        public static CollectionHolder Instance
        {
            get
            {
                if (instance == null)
                    instance = new CollectionHolder();

                return instance;
            }
        }

        public ObservableCollection<ItemDto> Items
        {
            get
            {
                if (items == null)
                    items = new ObservableCollection<ItemDto>(itemService.GetAllItems().ToList());

                return items;
            }
        }

        #endregion
    }
}
