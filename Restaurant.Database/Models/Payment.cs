using System;

namespace Restaurant.Database.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public decimal Total { get; set; }
        
        public DateTime Date { get; set; }

        public int UserId { get; set; }
        
        public User User { get; set; }
    }
}
