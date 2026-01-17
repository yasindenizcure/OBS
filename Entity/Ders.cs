using System.ComponentModel.DataAnnotations;

namespace DenemeDers.Entity
{
    public class Ders
    {
        [Key]
        public int DersId { get; set; }
        [Required(ErrorMessage = "Ders adı zorunludur.")]
        public string DersAdi { get; set; }
        public int SinifDuzeyi { get; set; }
        public string DersTuru { get; set; } = "Zorunlu";
        public int? OgretimGorevlisiId { get; set; }
        public virtual OgretimGorevlisi? OgretimGorevlisi { get; set; }
        [Range(1, 18, ErrorMessage = "Bir dersin AKTS değeri 1 ile 18 arasında olmalıdır.")]
        public int AKTS { get; set; }
        public int BolumId { get; set; }
        public virtual Bolum? Bolum { get; set; }
    }
}
