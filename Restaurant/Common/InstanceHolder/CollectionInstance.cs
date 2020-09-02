using System;
using System.Linq;
using System.Windows;
using Restaurant.Services;
using Restaurant.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Restaurant.Services.Models.Role;
using Restaurant.Services.Models.Item;
using Restaurant.Services.Implementations;

namespace Restaurant.Common.InstanceHolder
{
    public sealed class CollectionInstance
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
                {
                    try
                    {
                        users = new ObservableCollection<UserViewModel>(userService
                                                                    .GetAllUsers()
                                                                    .Select(u => new UserViewModel()
                                                                    {
                                                                        User = u
                                                                    }).ToList());
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Грешка с базата ! Опитайте отново !");
                    }
                }

                return users;
            }
        }

        public ObservableCollection<RoleDto> Roles
        {
            get
            {
                if (roles == null)
                {
                    try
                    {
                        roles = new ObservableCollection<RoleDto>(roleService.GetAllRoles().ToList());
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Грешка с базата ! Опитайте отново !");
                    }
                }

                return roles;
            }
        }

        public ObservableCollection<TableViewModel> Tables
        {
            get
            {
                if (tables == null)
                {
                    try
                    {
                        List<TableViewModel> tableList = tableService
                                                    .GetAllTables()
                                                    .Select(t => new TableViewModel()
                                                    {
                                                        Table = t
                                                    }).ToList();

                        tables = new ObservableCollection<TableViewModel>(tableList);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Грешка с базата ! Опитайте отново !");
                    }
                }

                return tables;
            }
        }

        public ObservableCollection<ItemDto> Items
        {
            get
            {
                if (items == null)
                {
                    try
                    {
                        items = new ObservableCollection<ItemDto>(itemService.GetAllItems().ToList());
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Грешка с базата ! Опитайте отново !");
                    }
                }

                return items;
            }
        }

        #endregion
    }
}
