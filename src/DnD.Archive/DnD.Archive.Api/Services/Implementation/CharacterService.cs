using AutoMapper;
using DnD.Archive.Api.DTOs.Request;
using DnD.Archive.Api.DTOs.Response;
using DnD.Archive.Api.Models;
using DnD.Archive.Api.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DnD.Archive.Api.Services.Implementation
{
    public class CharacterService : ICharacterService
    {
        private readonly DnDArchiveContext _context;
        private readonly IMapper _mapper;

        public CharacterService(DnDArchiveContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CharacterDTOResponse>> GetCharactersAsync()
        {
            var characters = await _context.Characters
                .Include(c => c.Specialization)
                .Include(c => c.Skills)
                .ToListAsync();

            List<CharacterDTOResponse> result = new List<CharacterDTOResponse>();
            foreach (var character in characters)
            {
                CharacterDTOResponse mappedCharacter = _mapper.Map<CharacterDTOResponse>(character);
                result.Add(mappedCharacter);
            }

            return result;
        }

        public async Task<IEnumerable<CharacterDTOResponse>> GetCharactersBySpecializationAsync(string specializationName)
        {
            var characters = await _context.Characters
                .Include(c => c.Skills)
                .Where(c => c.SpecializationName.ToLower().Equals(specializationName.Trim().ToLower()))
                .ToListAsync();

            List<CharacterDTOResponse> result = new List<CharacterDTOResponse>();
            foreach (var character in characters)
            {
                CharacterDTOResponse mappedCharacter = _mapper.Map<CharacterDTOResponse>(character);
                result.Add(mappedCharacter);
            }

            return result;
        }

        public async Task<CharacterDTOResponse?> GetCharacterByIdAsync(Guid id)
        {
            var character = await _context.Characters
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id.Equals(id));

            return _mapper.Map<CharacterDTOResponse>(character);
        }

        public async Task<CharacterDTOResponse>? RemoveCharacterByIdAsync(Guid id)
        {
            var character = await _context.Characters
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id.Equals(id));

            return _mapper.Map<CharacterDTOResponse>(character);
        }

        public async Task<Guid> AddCharacterAsync(CharacterDTORequest character)
        {
            Character toAdd = _mapper.Map<Character>(character);
            Specialization specializationToAdd = await _context.Specializations.FindAsync(character.SpecializationName);

            toAdd.Specialization = specializationToAdd;
            toAdd.SpecializationName = specializationToAdd.Name;

            await _context.Characters.AddAsync(toAdd);
            await _context.SaveChangesAsync();

            return toAdd.Id;
        }

        public async Task<bool> DeleteCharacterByIdAsync(Guid id)
        {
            var characterToRemove = await _context.Characters.FindAsync(id);

            if (characterToRemove == null) return false;

            _context.Characters.Remove(characterToRemove);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<CharacterDTOResponse?> UpdateCharacterByIdAsync(Guid id, CharacterDTORequest character)
        {
            var characterToUpdate = await _context.Characters.FindAsync(id);

            if (characterToUpdate == null) return null;

            characterToUpdate.Name = character.Name;
            characterToUpdate.Bio = character.Bio;
            characterToUpdate.Manapool = character.Manapool;
            characterToUpdate.Hitpoints = character.Hitpoints;
            characterToUpdate.SpecializationName = character.SpecializationName;
            characterToUpdate.Specialization = await _context.Specializations.FindAsync(character.SpecializationName);

            await _context.SaveChangesAsync();

            return _mapper.Map<CharacterDTOResponse>(character);
        }
    }
}
