using Restaurant.Common.InstanceHolder;
using Restaurant.Services.Models.Payment;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Restaurant.ViewModels
{
    public class ReportViewModel: BaseViewModel
    {
        #region Declarations

        private DateTime fromDate;
        private DateTime toDate;
        private ObservableCollection<UserViewModel> users;
        private ObservableCollection<PaymentDto> reports;
        private object selectedChoice;

        #endregion

        #region Properties

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

        public ObservableCollection<UserViewModel> Users
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

                if (value is UserViewModel userViewModel)
                    Reports = GetReport(FromDate, ToDate, userViewModel.User.Name);
                else
                    Reports = GetReport(FromDate, ToDate);
            }
        }

        #endregion

        #region Methods

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
