using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalonCruella.Models
{
    public class Salon
    {
        [Key] // Birincil anahtar tanımlaması
        public int Id { get; set; }

        [Required(ErrorMessage = "Salon adı zorunludur.")] // Boş bırakılamaz
        [StringLength(100, ErrorMessage = "Salon adı en fazla 100 karakter olabilir.")] // Maksimum uzunluk sınırı
        public string Adi { get; set; }

        [Required(ErrorMessage = "Adres zorunludur.")]
        [StringLength(250, ErrorMessage = "Adres en fazla 250 karakter olabilir.")]
        public string Adres { get; set; }

        // Navigation Property: Bir salonda birden fazla çalışan olabilir
        public ICollection<Calisan> Calisanlar { get; set; }
    }
}
