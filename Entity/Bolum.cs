namespace DenemeDers.Entity
{
    public class Bolum
    {
        public int BolumId { get; set; }
        public string? BolumAdi { get; set; }
        public virtual ICollection<Ders>? Dersler { get; set; }
    }
}
