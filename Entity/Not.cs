using System;
using System.ComponentModel.DataAnnotations;

namespace DenemeDers.Entity
{
    public class Not
    {
        [Key]
        public int NotId { get; set; }

        [Required]
        public int OgrenciId { get; set; }

        [Required]
        public int DersId { get; set; }

        [Required]
        public int OgretimGorevlisiId { get; set; }

        [Range(0, 100)]
        public int VizeNotu { get; set; }

        [Range(0, 100)]
        public int FinalNotu { get; set; }

        public double Ortalama => (VizeNotu * 0.4) + (FinalNotu * 0.6);

        public DateTime KayitTarihi { get; set; } = DateTime.Now;

        public virtual Ogrenci Ogrenci { get; set; }
        public virtual OgretimGorevlisi OgretimGorevlisi { get; set; }
        public virtual Ders Ders { get; set; }

    }
}