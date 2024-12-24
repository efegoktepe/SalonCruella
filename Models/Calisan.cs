using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalonCruella.Models
{
    public class Calisan
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Çalışan adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Çalışan adı en fazla 100 karakter olabilir.")]
        public string Adi { get; set; }

        [Required(ErrorMessage = "Uzmanlık alanı zorunludur.")]
        [StringLength(100, ErrorMessage = "Uzmanlık alanı en fazla 100 karakter olabilir.")]
        public string UzmanlikAlani { get; set; }

        [Required(ErrorMessage = "Başlangıç saati zorunludur.")]
        public TimeSpan BaslangicSaati { get; set; }

        [Required(ErrorMessage = "Bitiş saati zorunludur.")]
        public TimeSpan BitisSaati { get; set; }

        [ForeignKey("Salon")]
        [Required(ErrorMessage = "Salon seçimi zorunludur.")]
        public int SalonId { get; set; }

        public virtual Salon Salon { get; set; }
    }
}
