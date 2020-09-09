using Restaurant.Database.Models;
using Restaurant.Database.Services;
using Restaurant.Database.Services.Implementations;
using Restaurant.Services.Models.Payment;
using Restaurant.Services.Models.User;
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

        public PaymentDto CreatePayment(decimal total, DateTime date, UserDto userDto)
        {
            Payment payment = paymentDb.CreatePayment(total, date, userDto.Id);

            return new PaymentDto()
            {
                Id = payment.Id,
                Name = userDto.Name,
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
