using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalonCruella.Models
{
    public class Randevu
    {
        [Key] // Birincil anahtar
        public int Id { get; set; }

        [Required(ErrorMessage = "Tarih zorunludur.")] // Boş bırakılamaz
        public DateTime Tarih { get; set; }

        [Required(ErrorMessage = "Saat zorunludur.")]
        [StringLength(5, ErrorMessage = "Saat formatı HH:mm şeklinde olmalıdır.")]
        public string Saat { get; set; }

        [Required(ErrorMessage = "Müşteri adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Müşteri adı en fazla 100 karakter olabilir.")]
        public string MusteriAdi { get; set; }

        // Yabancı Anahtar: Calisan ile ilişki
        [ForeignKey("Calisan")]
        public int CalisanId { get; set; }
        public virtual Calisan Calisan { get; set; } // Navigation Property

        // Yabancı Anahtar: Salon ile ilişki
        [ForeignKey("Salon")]
        public int SalonId { get; set; }
        public virtual Salon Salon { get; set; } // Navigation Property
    }
}
