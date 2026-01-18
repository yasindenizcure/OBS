namespace DenemeDers.Entity
{
    public class Duyuru
    {
        public int DuyuruId { get; set; }
        public string? Baslik {  get; set; }
        public string? Icerik { get; set; }
        public DateTime Tarih {  get; set; } = DateTime.Now;
        public string? Olusturan { get; set; } 
    }
}
