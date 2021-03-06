﻿using Prism.Commands;
using Restaurant.Common.InstanceHolder;
using Restaurant.Services.Models.Payment;
using Restaurant.Services.Models.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Restaurant.ViewModels
{
    public class ReportViewModel: BaseViewModel
    {
        #region Declarations

        private DelegateCommand<object> returnCommand;
        private DateTime fromDate;
        private DateTime toDate;
        private ObservableCollection<UserDto> users;
        private ObservableCollection<PaymentDto> reports;
        private object selectedChoice;

        #endregion

        #region Properties

        public DelegateCommand<object> ReturnCommand
        {
            get
            {
                if (returnCommand == null)
                    returnCommand = new DelegateCommand<object>(Return);

                return returnCommand;
            }
        }

        public DateTime FromDate
        {
            get
            {
                if (fromDate == default(DateTime))
                    fromDate = DateTime.Now.AddDays(-1);

                return fromDate;
            }
            set
            {
                if(value <= ToDate) 
                    fromDate = value;

                Reports = GetReport(FromDate, ToDate);
            }
        }

        public DateTime ToDate
        {
            get
            {
                if (toDate == default(DateTime))
                    toDate = DateTime.Now;

                return toDate;
            }
            set
            {
                if(value >= fromDate)
                    toDate = value;

                Reports = GetReport(FromDate, ToDate);
            }
        }

        public ObservableCollection<UserDto> Users
        {
            get
            {
                if (users == null)
                    users = CollectionInstance.Instance.Users;

                return users;
            }
        }

        public ObservableCollection<PaymentDto> Reports
        {
            get
            {
                if (reports == null)
                    reports = GetReport(FromDate,ToDate);

                return reports;
            }
            private set
            {
                reports = value;

                OnPropertyChanged("Reports");
            }
        }

        public object SelectedChoice
        {
            get => selectedChoice;
            set
            {
                selectedChoice = value;

                if (value is UserDto userDto)
                    Reports = GetReport(FromDate, ToDate, userDto.Name);
                else
                    Reports = GetReport(FromDate, ToDate);
            }
        }

        #endregion

        #region Methods

        private void Return(object obj)
        {
            MenuViewModel.Instance.ChangeMenuViewCommand.Execute(MenuViewModel.Instance.AdminPanelViewModel);
        }

        public ObservableCollection<PaymentDto> GetReport(DateTime fromDate, DateTime toDate, string userName)
        {
            List<PaymentDto> reports = CollectionInstance.Instance
                                                         .Payments
                                                         .Where(r => r.Date.Date >= fromDate.Date && r.Date.Date <= toDate.Date)
                                                         .Where(r => r.Name == userName)
                                                         .ToList();

            return new ObservableCollection<PaymentDto>(reports);
        }

        public ObservableCollection<PaymentDto> GetReport(DateTime fromDate, DateTime toDate)
        {
            List<PaymentDto> reports = CollectionInstance.Instance
                                                         .Payments
                                                         .Where(r => r.Date.Date >= fromDate.Date && r.Date.Date <= toDate.Date)
                                                         .ToList();

            return new ObservableCollection<PaymentDto>(reports);
        }

        #endregion
    }
}
