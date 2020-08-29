using System.Linq;
using Restaurant.Services;
using Restaurant.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Restaurant.Services.Models.Item;
using Restaurant.Services.Implementations;
using Restaurant.Services.Models.RoleModels;

namespace Restaurant.Common.InstanceHolder
{
    public class CollectionInstance
    {
        #region Declarations

        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly ITableService tableService;
        private readonly IItemService itemService;
        private static CollectionInstance instance = null;
        private ObservableCollection<UserViewModel> users;
        private ObservableCollection<RoleDto> roles;
        private ObservableCollection<TableViewModel> tables;
        private ObservableCollection<ItemDto> items;

        #endregion

        #region Constructors

        private CollectionInstance()
        {
            userService = new UserService();
            roleService = new RoleService();
            tableService = new TableService();
            itemService = new ItemService();
        }

        #endregion

        #region Properties

        public static CollectionInstance Instance
        {
            get
            {
                if (instance == null)
                    instance = new CollectionInstance();

                return instance;
            }
        }

        public ObservableCollection<UserViewModel> Users
        {
            get
            {
                if (users == null)
                    users = new ObservableCollection<UserViewModel>(userService
                                                                    .GetAllUsers()
                                                                    .Select(u => new UserViewModel()
                                                                    {
                                                                        User = u
                                                                    }).ToList());

                return users;
            }
        }

        public ObservableCollection<RoleDto> Roles
        {
            get
            {
                if (roles == null)
                    roles = new ObservableCollection<RoleDto>(roleService.GetAllRoles().ToList());

                return roles;
            }
        }

        public ObservableCollection<TableViewModel> Tables
        {
            get
            {
                if (tables == null)
                {
                    List<TableViewModel> tableList = tableService
                                                    .GetAllTables()
                                                    .Select(t => new TableViewModel()
                                                    {
                                                        Table = t,
                                                        IsTaken = t.IsTaken
                                                    }).ToList();

                    tables = new ObservableCollection<TableViewModel>(tableList);
                }

                return tables;
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
