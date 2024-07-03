namespace pruebabackend.Models
{
    public partial class Payment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public int Idrent { get; set; }

        public virtual Rent IdrentNavigation { get; set; }
    }
}
