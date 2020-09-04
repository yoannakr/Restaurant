using Restaurant.Database.Models;
using Restaurant.Database.Services;
using Restaurant.Database.Services.Implementations;
using Restaurant.Services.Models.Payment;
using System;
using System.Linq;

namespace Restaurant.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        #region Declarations

        private readonly IPaymentDbService paymentDb;

        #endregion

        #region Constructors

        public PaymentService()
        {
            paymentDb = new PaymentDbService();
        }

        #endregion

        #region Methods

        public PaymentDto CreatePayment(decimal total, DateTime date, User user)
        {
            Payment payment = paymentDb.CreatePayment(total, date, user.Id);

            return new PaymentDto()
            {
                Id = payment.Id,
                Name = user.Name,
                Total = payment.Total,
                Date = payment.Date
            };
        }

        public IQueryable<PaymentDto> GetAllPayments()
        {
            return paymentDb.GetAllPayments().Select(p => new PaymentDto()
            {
                Id = p.Id,
                Name = p.User.Name,
                Total = p.Total,
                Date = p.Date
            });
        }

        #endregion
    }
}
