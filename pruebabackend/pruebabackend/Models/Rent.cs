namespace pruebabackend.Models
{
    public partial class Rent
    {
        public Rent()
        {
            Payments = new HashSet<Payment>();
        }

        public int Id { get; set; }
        public string Idclient { get; set; }
        public string Idplate { get; set; }
        public DateTime Date { get; set; }
        public int Time { get; set; }
        public decimal TotalValue { get; set; }
        public decimal Balance { get; set; }
        public decimal InitialPayment { get; set; }
        public bool Returned { get; set; }
        public bool RegisterState { get; set; }

        public virtual Car IdplateNavigation { get; set; }
        public virtual Client IdclientNavigation { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
