using Restaurant.Database.Models;
using System;
using System.Linq;

namespace Restaurant.Database.Services
{
    public interface IPaymentDbService
    {
        Payment CreatePayment(decimal total, DateTime date, int userId);

        IQueryable<Payment> GetAllPayments();
    }
}
