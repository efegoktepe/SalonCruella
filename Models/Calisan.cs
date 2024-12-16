using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalonCruella.Models
{
    public class Calisan
    {
        [Key] // Birincil anahtar tanımlaması
        public int Id { get; set; }

        [Required(ErrorMessage = "Çalışan adı zorunludur.")] // Boş bırakılamaz
        [StringLength(100, ErrorMessage = "Çalışan adı en fazla 100 karakter olabilir.")]
        public string Adi { get; set; }

        [Required(ErrorMessage = "Uzmanlık alanı zorunludur.")]
        [StringLength(100, ErrorMessage = "Uzmanlık alanı en fazla 100 karakter olabilir.")]
        public string UzmanlikAlani { get; set; }

        // Yabancı Anahtar: Hangi salona bağlı olduğunu belirtir
        [ForeignKey("Salon")]
        public int SalonId { get; set; }

        // Navigation Property: Bağlantılı salon bilgisi
        public Salon Salon { get; set; }
    }
}
