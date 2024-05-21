using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD.Archive.Common.Database.Models
{
    public class Character
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [MaxLength(500)]
        public string Bio { get; set; } = null!;
        
        [Required]
        public int Hitpoints { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Manacost must be a positive value.")]
        public int Manapool { get; set; }

        [Required]
        public Specialization Specialization { get; set; } = null!;
        
        public ICollection<Skill> Skills { get; set; } = null!;

    }
}
