using AutoMapper;
using DnD.Archive.Api.DTOs.Response;
using DnD.Archive.Api.Models;
using DnD.Archive.Api.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DnD.Archive.Api.Services.Implementation
{
    public class SpecializationService : ISpecializationService
    {
        private readonly DnDArchiveContext _context;
        private readonly IMapper _mapper;

        public SpecializationService(DnDArchiveContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SpecializationDTO?> GetSpecializationByNameAsync(string specializationName)
        {
            var result = await _context.Specializations
                .FirstOrDefaultAsync(c => c.Name.Equals(specializationName));

            return _mapper.Map<SpecializationDTO>(result);
        }
    }
}
