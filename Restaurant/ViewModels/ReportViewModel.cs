using Restaurant.Common.InstanceHolder;
using Restaurant.Services.Models;
using Restaurant.Services.Models.Payment;
using System;
using System.Collections.ObjectModel;

namespace Restaurant.ViewModels
{
    public class ReportViewModel: BaseViewModel
    {
        #region Declarations

        private DateTime fromDate;
        private DateTime toDate;
        private ObservableCollection<UserViewModel> users;
        private ObservableCollection<PaymentDto> reports;

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
                fromDate = value;
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
                toDate = value;
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
                    reports = CollectionInstance.Instance.Payments;

                return reports;
            }
        }

        #endregion
    }
}
