using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Path { get; set; } = null!;

        public int? MotelId { get; set; }
        public int? SuiteId { get; set; }

        [ForeignKey("MotelId")]
        public Motel? Motel { get; set; }

        [ForeignKey("SuiteId")]
        public Suite? Suite { get; set; }
    }
}