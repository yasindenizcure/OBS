using System.ComponentModel.DataAnnotations;

namespace DenemeDers.Entity
{
    public class OgretimGorevlisi
    {
        [Key]
        public int OgretimGorevlisiId { get; set; }
        public string? Ad { get; set; }
        public string? Soyad { get; set; }
        public string? Unvan { get; set; }
        public int BolumId { get; set; }
        public virtual Bolum Bolum { get; set; }
        public virtual ICollection<Ders> Dersler { get; set; } = new List<Ders>();
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
