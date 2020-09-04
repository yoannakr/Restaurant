﻿using Restaurant.Database.Models;
using Restaurant.Services.Models.Payment;
using System;
using System.Linq;

namespace Restaurant.Services
{
    public interface IPaymentService
    {
        PaymentDto CreatePayment(decimal total, DateTime date, User user);

        IQueryable<PaymentDto> GetAllPayments();
    }
}
