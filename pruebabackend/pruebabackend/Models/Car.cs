namespace pruebabackend.Models
{
    public partial class Car
    {
        public Car()
        {
            Rents = new HashSet<Rent>();
        }

        public string Plate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Cost { get; set; }
        public bool Available { get; set; }

        public virtual ICollection<Rent> Rents { get; set; }
    }
}
