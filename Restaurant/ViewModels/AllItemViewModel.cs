﻿using System.Linq;
using Prism.Commands;
using Restaurant.Services;
using Restaurant.Database.Models;
using System.Collections.Generic;
using Restaurant.Services.Models.Item;
using Restaurant.Services.Implementations;

namespace Restaurant.ViewModels
{
    public class AllItemViewModel : BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> addItemToSelected;
        private readonly IItemService itemService;
        private List<ItemDto> items;

        #endregion

        #region Constructor

        public AllItemViewModel(SalesViewModel salesViewModel)
        {
            itemService = new ItemService();
            SelectedItemViewModel = salesViewModel.SelectedItemViewModel;
        }

        #endregion

        #region Properties

        public DelegateCommand<object> AddItemToSelected
        {
            get
            {
                if (addItemToSelected == null)
                    addItemToSelected = new DelegateCommand<object>(AddItem);

                return addItemToSelected;
            }
        }

        public List<ItemDto> Items
        {
            get
            {
                if (items == null)
                    items = new List<ItemDto>();

                items = itemService.GetAllItems().ToList();

                return items;

            }
        }

        public SelectedItemViewModel SelectedItemViewModel { get; set; }

        #endregion

        #region Methods

        private void AddItem(object obj)
        {
            ItemDto itemDto = obj as ItemDto;

            RowItemViewModel selectedItem = SelectedItemViewModel.SelectedItem;

            if (selectedItem != null)
            {
                if(selectedItem.Item.Id == itemDto.Id)
                {
                    selectedItem.Count += 1;

                    return;
                }

                RowItemViewModel sameExtra = SelectedItemViewModel
                                             .SelectedItem
                                             .Extras
                                             .Where(e => e.Item.Id == itemDto.Id)
                                             .FirstOrDefault();

                if (sameExtra != null)
                {
                    sameExtra.Count += 1;

                    return;
                }

                RowItemViewModel extra = new RowItemViewModel(SelectedItemViewModel)
                {
                    Item = new Item()
                    {
                        Id = itemDto.Id,
                        Name = itemDto.Name,
                        Price = itemDto.Price
                    },
                    Count = 1,
                    Total = itemDto.Price,
                    RowItemViewModelex = SelectedItemViewModel.SelectedItem
                };

                SelectedItemViewModel.SelectedItem.Extras.Add(extra);

                selectedItem.ShowListBtnVisibility = true;
            }
            else
            { 
                RowItemViewModel item = new RowItemViewModel(SelectedItemViewModel)
                {
                    Item = new Item()
                    {
                        Id = itemDto.Id,
                        Name = itemDto.Name,
                        Price = itemDto.Price
                    },
                    Count = 1,
                    Total = itemDto.Price
                };

                SelectedItemViewModel.Items.Add(item);

            }

        }

        #endregion
    }
}
