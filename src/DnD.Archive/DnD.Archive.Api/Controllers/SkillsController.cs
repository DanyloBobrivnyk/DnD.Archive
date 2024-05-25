using DnD.Archive.Api.Services.Abstract;
using DnD.Archive.Api.Services.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DnD.Archive.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillsController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        // POST: api/Skills/{characterGuid}/{skillGuid}
        [HttpPost]
        [Route("api/[controller]/{characterGuid}/{skillGuid}")]
        public async Task<ActionResult> AddSkillByCharacterId(Guid characterGuid, Guid skillGuid)
        {
            if (characterGuid.Equals(Guid.Empty) || skillGuid.Equals(Guid.Empty))
            {
                return BadRequest("Guids can't be empty");
            }

            var res = await _skillService.AddSkillByCharacterIdAsync(characterGuid, skillGuid);

            //TODO: Add error message processing.
            return !res ? NotFound("Can't performe skill add with provided ids.") : Ok(res);
        }
    }
}
