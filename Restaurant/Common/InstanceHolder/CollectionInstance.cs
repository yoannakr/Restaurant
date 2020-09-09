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
using Restaurant.Services.Models.Category;
using Restaurant.Services.Models.Payment;
using Restaurant.Services.Models.User;

namespace Restaurant.Common.InstanceHolder
{
    public sealed class CollectionInstance
    {
        #region Declarations

        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly ITableService tableService;
        private readonly IItemService itemService;
        private readonly ICategoryService categoryService;
        private readonly IPaymentService paymentService;
        private static CollectionInstance instance = null;
        private ObservableCollection<UserDto> users;
        private ObservableCollection<RoleDto> roles;
        private ObservableCollection<TableViewModel> tables;
        private ObservableCollection<ItemDto> items;
        private ObservableCollection<CategoryDto> categories;
        private ObservableCollection<PaymentDto> payments;

        #endregion

        #region Constructors

        private CollectionInstance()
        {
            userService = new UserService();
            roleService = new RoleService();
            tableService = new TableService();
            itemService = new ItemService();
            categoryService = new CategoryService();
            paymentService = new PaymentService();
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

        public ObservableCollection<UserDto> Users
        {
            get
            {
                if (users == null)
                {
                    try
                    {
                        users = new ObservableCollection<UserDto>(userService.GetAllUsers());
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
                        roles = new ObservableCollection<RoleDto>(roleService.GetAllRoles());
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
                                                        TableDto = t
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
                        items = new ObservableCollection<ItemDto>(itemService.GetAllItems());
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Грешка с базата ! Опитайте отново !");
                    }
                }

                return items;
            }
        }

        public ObservableCollection<CategoryDto> Categories
        {
            get
            {
                if (categories == null)
                {
                    try
                    {
                        categories = new ObservableCollection<CategoryDto>(categoryService.GetAllCategories());
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Грешка с базата ! Опитайте отново !");
                    }
                }

                return categories;
            }
        }

        public ObservableCollection<PaymentDto> Payments
        {
            get
            {
                if (payments == null)
                {
                    try
                    {
                        payments = new ObservableCollection<PaymentDto>(paymentService.GetAllPayments());
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Грешка с базата ! Опитайте отново !");
                    }
                }

                return payments;
            }
        }

        #endregion
    }
}
