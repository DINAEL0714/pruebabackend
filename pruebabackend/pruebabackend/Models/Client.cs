namespace pruebabackend.Models
{
    public partial class Client
    {
        public Client()
        {
            Rents = new HashSet<Rent>();
        }

        public int Idclient { get; set; }
        public string Idcard { get; set; }
        public string Name { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }

        public virtual ICollection<Rent> Rents { get; set; }
    }
}
