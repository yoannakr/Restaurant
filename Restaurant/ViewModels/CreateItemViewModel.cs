﻿using System;
using Prism.Commands;
using Restaurant.Services;
using System.Windows.Input;
using Restaurant.Services.Implementations;

namespace Restaurant.ViewModels
{
    public class CreateItemViewModel : BaseViewModel
    {
        #region Declarations

        private ICommand browseCommand;
        private DelegateCommand<object> createItemCommand;
        private readonly IItemService itemService;
        private string imageSource;
        private byte[] imageContent;

        #endregion

        #region Constructor

        public CreateItemViewModel()
        {
            itemService = new ItemService();
        }

        #endregion

        #region Properties

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageSource 
        {
            get => imageSource;
            set
            {
                imageSource = value;
                OnPropertyChanged("ImageSource");
            }
        }

        public byte[] ImageContent 
        {
            get => imageContent;
            set
            {
                imageContent = value;
                CreateItemCommand.RaiseCanExecuteChanged();
            }
        }

        public ICommand BrowseCommand
        {
            get
            {
                if (browseCommand == null)
                    browseCommand = new DelegateCommand<object>(BrowseFolder);

                return browseCommand;
            }
        }

        public DelegateCommand<object> CreateItemCommand
        {
            get
            {
                if (createItemCommand == null)
                    createItemCommand = new DelegateCommand<object>(CreateItem,CanExecute);

                return createItemCommand;
            }
        }

        #endregion

        #region Methods

        private void BrowseFolder(object obj)
        {
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            openFileDlg.Filter = "Image files (*.png) | *.png";

            Nullable<bool> result = openFileDlg.ShowDialog();

            if (result == true)
            {
                ImageSource = openFileDlg.FileName;
                ImageContent = System.IO.File.ReadAllBytes(openFileDlg.FileName);
            }
        }

        private void CreateItem(object obj)
        {
            itemService.CreateItem(Name, Price, ImageContent);
        }

        private bool CanExecute(object arg)
        {
            return IsValid();
        }

        private bool IsValid()
        {
            if (Name == null || Price == 0 || ImageContent == null)
                return false;

            return true;
        }

        #endregion
    }
}
