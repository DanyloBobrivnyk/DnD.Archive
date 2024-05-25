using DnD.Archive.Api.DTOs.Request;
using DnD.Archive.Api.DTOs.Response;

namespace DnD.Archive.Api.Services.Abstract
{
    public interface ICharacterService
    {
        Task<IEnumerable<CharacterDTOResponse>> GetCharactersAsync();
        Task<IEnumerable<CharacterDTOResponse>> GetCharactersBySpecializationAsync(string specializationName);
        Task<CharacterDTOResponse?> GetCharacterByIdAsync(Guid id);
        Task<CharacterDTOResponse>? RemoveCharacterByIdAsync(Guid id);
        Task<Guid> AddCharacterAsync(CharacterDTORequest character);
        Task<Boolean> DeleteCharacterByIdAsync(Guid id);
        Task<CharacterDTOResponse?> UpdateCharacterByIdAsync(Guid id, CharacterDTORequest character);
    }
}
