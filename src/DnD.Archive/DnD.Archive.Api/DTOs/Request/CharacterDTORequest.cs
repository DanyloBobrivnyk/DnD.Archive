using System.ComponentModel.DataAnnotations;

namespace DnD.Archive.Api.DTOs.Request
{
    public class CharacterDTORequest
    {
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
        public string SpecializationName { get; set; } = null!;
    }
}
