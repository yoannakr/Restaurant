namespace Restaurant.Services.Models.Table
{
    public class TableDto
    {
        #region Properties

        public int Id { get; set; }

        public int Number { get; set; }

        public int Seats { get; set; }

        public bool IsTaken { get; set; }

        #endregion
    }
}
