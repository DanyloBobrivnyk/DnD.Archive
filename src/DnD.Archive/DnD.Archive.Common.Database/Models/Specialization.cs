using System.ComponentModel.DataAnnotations;

namespace DnD.Archive.Common.Database.Models
{
    public class Specialization
    {
        [Key]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
    }
}
