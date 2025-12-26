namespace DenemeDers.Entity
{
    public class Ogrenci
    {
        public int OgrenciId { get; set; }
        public string? Ad { get; set; }
        public string? Soyad { get; set; }
        public int Yas { get; set; }
        public int Sinif { get; set; }
        public string? Bolum { get; set; }
        public string? OgrNo { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
