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
        public virtual OgretimGorevlisi OgretimGorevlisi { get; set; }
    }
}
