using System;
using Prism.Commands;
using System.Windows;
using Restaurant.Views;
using Restaurant.Services;
using Restaurant.Database.Models;
using System.Collections.ObjectModel;
using Restaurant.Services.Models.Item;
using Restaurant.Common.InstanceHolder;
using Restaurant.Services.Implementations;

namespace Restaurant.ViewModels
{
    public class UpdateOrDeleteItemViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> updateItemCommand;
        private DelegateCommand<object> deleteItemCommand;
        private readonly IItemService itemService;
        private UpdateItemViewModel updateItemViewModel;
        private ObservableCollection<ItemDto> items;

        #endregion

        #region Constructors

        public UpdateOrDeleteItemViewModel()
        {
            itemService = new ItemService();
        }

        #endregion

        #region Properties

        public DelegateCommand<object> UpdateItemCommand
        {
            get
            {
                if (updateItemCommand == null)
                    updateItemCommand = new DelegateCommand<object>(UpdateItem);

                return updateItemCommand;
            }
        }

        public DelegateCommand<object> DeleteItemCommand
        {
            get
            {
                if (deleteItemCommand == null)
                    deleteItemCommand = new DelegateCommand<object>(DeleteItem);

                return deleteItemCommand;
            }
        }

        public ObservableCollection<ItemDto> Items
        {
            get
            {
                if (items == null)
                    items = CollectionInstance.Instance.Items;

                return items;
            }
        }

        public UpdateItemViewModel UpdateItemViewModel
        {
            get
            {
                updateItemViewModel = new UpdateItemViewModel(Item);
                UpdateItemView updateItemView = new UpdateItemView();

                updateItemViewModel.View = updateItemView;

                updateItemView.DataContext = updateItemViewModel;

                return updateItemViewModel;
            }
        }

        public ItemDto Item { get; set; }

        #endregion

        #region Methods

        private void UpdateItem(object obj)
        {
            ItemDto item = obj as ItemDto;

            if (item == null)
            {
                MessageBox.Show("Грешка в системата !");
                return;
            }

            Item = item;

            MenuViewModel.Instance.ChangeMenuViewCommand.Execute(UpdateItemViewModel);
        }

        private void DeleteItem(object obj)
        {
            ItemDto item = obj as ItemDto;

            if (item == null)
            {
                MessageBox.Show("Грешка в системата !");
                return;
            }

            if (MessageBox.Show("Сигурни ли сте, че искате да изтриете този продукт ?",
                    "Потвърждение", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;

            try
            {
                Item deletedItem = new Item()
                {
                    Id = item.Id
                };

                itemService.DeleteItem(deletedItem);
            }
            catch (Exception)
            {
                MessageBox.Show("Грешка с базата ! Опитайте отново !");
                return;
            }

            CollectionInstance.Instance.Items.Remove(item);
        }

        #endregion
    }
}
