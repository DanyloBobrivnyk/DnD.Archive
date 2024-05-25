using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DnD.Archive.Api.Models
{
    public class Skill
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Manacost must be a positive value.")]
        public int Manacost { get; set; }

        [Required]
        public ICollection<Specialization> AllowedSpecialization { get; set; } = new List<Specialization>();

        public int HealthImpact { get; set; }

        public ICollection<Character> Characters { get; set; } = new List<Character>();
    }
}