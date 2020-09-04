using Restaurant.Database.Data;
using Restaurant.Database.Models;
using System;
using System.Linq;

namespace Restaurant.Database.Services.Implementations
{
    public class PaymentDbService : IPaymentDbService
    {
        #region Declarations

        private readonly RestaurantDbContext context;

        #endregion

        #region Constructors

        public PaymentDbService()
        {
            context = new RestaurantDbContext();
        }

        #endregion

        #region Methods

        public Payment CreatePayment(decimal total, DateTime date, int userId)
        {
            Payment payment = new Payment()
            {
                Total = total,
                Date = date,
                UserId = userId
            };

            context.Payments.Add(payment);
            context.SaveChanges();

            return payment;
        }

        public IQueryable<Payment> GetAllPayments()
        {
            return context.Payments;
        }

        #endregion
    }
}
