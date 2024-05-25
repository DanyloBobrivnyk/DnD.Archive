namespace DnD.Archive.Api.DTOs.Response
{
    public class CharacterDTOResponse
    {
        public string Name { get; set; } = null!;
        public string Bio { get; set; } = null!;
        public int Hitpoints { get; set; }
        public int Manapool { get; set; }
        public string SpecializationName { get; set; } = null!;
        public ICollection<SkillDTO> Skills { get; set; } = new List<SkillDTO>();
    }
}
