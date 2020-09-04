using System;

namespace Restaurant.Services.Models.Payment
{
    public class PaymentDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Total { get; set; }

        public DateTime Date { get; set; }
    }
}
