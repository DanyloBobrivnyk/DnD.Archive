using AutoMapper;
using DnD.Archive.Api.DTOs.Response;
using DnD.Archive.Api.Models;
using DnD.Archive.Api.Services.Abstract;

namespace DnD.Archive.Api.Services.Implementation
{
    public class SkillService : ISkillService
    {
        private readonly DnDArchiveContext _context;
        private readonly IMapper _mapper;

        public SkillService(DnDArchiveContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Boolean> AddSkillByCharacterIdAsync(Guid characterGuid, Guid skillGuid)
        {
            var characterToUpdate = await _context.Characters.FindAsync(characterGuid);
            var skillToAdd = await _context.Skills.FindAsync(skillGuid);

            if (characterToUpdate.Skills.Contains(skillToAdd)) { return true; }
            if (characterToUpdate == null
                || skillToAdd == null
                || !skillToAdd.AllowedSpecialization.Contains(characterToUpdate.Specialization)) { return false; }

            characterToUpdate.Skills.Add(skillToAdd);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
