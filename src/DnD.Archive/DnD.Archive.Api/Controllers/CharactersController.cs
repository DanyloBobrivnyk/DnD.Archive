using DnD.Archive.Api.DTOs.Request;
using DnD.Archive.Api.DTOs.Response;
using DnD.Archive.Api.Services.Abstract;
using DnD.Archive.Api.Services.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DnD.Archive.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        private readonly ISpecializationService _specializationService;

        public CharactersController(ICharacterService characterService, ISpecializationService specializationService)
        {
            _characterService = characterService;
            _specializationService = specializationService;
        }

        // GET: api/Characters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterDTOResponse>>> GetCharacters()
        {
            var res = await _characterService.GetCharactersAsync();

            return res is null ? NotFound("Characters not found") : Ok(res);
        }

        // GET: api/Characters/{specializationName}
        [HttpGet("specializationName")]
        public async Task<ActionResult<IEnumerable<CharacterDTOResponse>>> GetCharactersBySpecialization(string specializationName)
        {
            var res = await _characterService.GetCharactersBySpecializationAsync(specializationName);

            return res.IsNullOrEmpty() ? NotFound("Characters with provided specialization not found") : Ok(res);
        }

        // GET: api/Characters/{guid}
        [HttpGet("guid")]
        public async Task<ActionResult> GetCharacterById(Guid guid)
        {
            if (guid.Equals(Guid.Empty))
            {
                return BadRequest("Guid can't be empty");
            }

            var res = await _characterService.GetCharacterByIdAsync(guid);

            return res is null ? NotFound("Character with provided id not found") : Ok(res);
        }

        // POST: api/Characters
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateCharacter([FromBody] CharacterDTORequest character)
        {
            bool isValidated = TryValidateModel(character);
            if (!isValidated)
            {
                return BadRequest("Valid character data should be provided");
            }

            try
            {
                var specialization = await _specializationService.GetSpecializationByNameAsync(character.SpecializationName);

                if (specialization == null)
                {
                    return BadRequest("Provided specialization not found.");
                }

                var res = await _characterService.AddCharacterAsync(character);

                return Created();
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error occured during character creation.", ex);
            }
        }

        //DELETE: api/Characters/{guid}
        [HttpDelete]
        public async Task<ActionResult> DeleteCharacterById(Guid guid)
        {
            if (guid.Equals(Guid.Empty))
            {
                return BadRequest("Guid can't be empty");
            }

            var res = await _characterService.DeleteCharacterByIdAsync(guid);

            return !res ? NotFound("Character with provided guid not found.") : Ok("Character was removed.");
        }

        //PUT: api/Characters/{guid}
        [HttpPut("guid")]
        public async Task<ActionResult> UpdateCharacterById(Guid guid, [FromBody] CharacterDTORequest character)
        {
            if (guid.Equals(Guid.Empty))
            {
                return BadRequest("Guid can't be empty");
            }

            try
            {
                var specialization = await _specializationService.GetSpecializationByNameAsync(character.SpecializationName);

                if (specialization == null)
                {
                    return BadRequest("Provided specialization not found.");
                }

                var res = await _characterService.UpdateCharacterByIdAsync(guid, character);

                return res is null ? NotFound("Character with provided id not found") : Ok(res);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error occured during character creation.", ex);
            }
            
        }
    }
}
