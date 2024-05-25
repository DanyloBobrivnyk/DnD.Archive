namespace DnD.Archive.Api.DTOs.Response
{
    public class SkillDTO
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int Manacost { get; set; }

        public int HealthImpact { get; set; }
    }
}
