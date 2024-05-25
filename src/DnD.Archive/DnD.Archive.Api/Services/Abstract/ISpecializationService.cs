using DnD.Archive.Api.DTOs.Response;

namespace DnD.Archive.Api.Services.Abstract
{
    public interface ISpecializationService
    {
        Task<SpecializationDTO?> GetSpecializationByNameAsync(string specializationName);
    }
}
