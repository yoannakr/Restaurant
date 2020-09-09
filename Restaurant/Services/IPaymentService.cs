using Restaurant.Database.Models;
using Restaurant.Services.Models.Payment;
using Restaurant.Services.Models.User;
using System;
using System.Linq;

namespace Restaurant.Services
{
    public interface IPaymentService
    {
        PaymentDto CreatePayment(decimal total, DateTime date, UserDto userDto);

        IQueryable<PaymentDto> GetAllPayments();
    }
}
