using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Models
{
    public class Suite
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MotelId { get; set; }

        [Required]
        [MaxLength(100)]
        public string SuiteName { get; set; } = null!; 

        [MaxLength(255)]
        public string? Description { get; set; }

        [Required]
        public decimal BasePrice { get; set; }

        [ForeignKey("MotelId")]
        public Motel? Motel { get; set; }
        
        public List<Image> Images { get; set; } = new();
    }
}