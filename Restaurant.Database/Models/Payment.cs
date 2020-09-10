using System;
using System.ComponentModel.DataAnnotations;
using static Restaurant.Database.Models.DataValidation.Payment;


namespace Restaurant.Database.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public decimal Total { get; set; }

        public DateTime Date { get; set; }

        [MaxLength(DesriptionMaxLength)]
        public string Description { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
