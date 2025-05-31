using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    public class Text
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)] // Başlık için uzunluk sınırı (isteğe göre ayarlanabilir)
        public string? Title { get; set; }

        [Required]
        [MaxLength(40000)] // ≈ 6000 kelime
        public string? Content { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Yeni eklenen alanlar
        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        // Fotoğraf verisi
        public byte[]? PhotoData { get; set; }

        // İstersen fotoğrafın içeriğinin tipi (MIME türü) için
        public string? PhotoContentType { get; set; }
    }
}
