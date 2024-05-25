using DnD.Archive.Api.DTOs.Request;

namespace DnD.Archive.Api.Services.Abstract
{
    public interface ISkillService
    {
        Task<Boolean> AddSkillByCharacterIdAsync(Guid characterGuid, Guid skillGuid);
    }
}
