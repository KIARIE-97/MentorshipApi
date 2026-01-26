using Mentorship.Api.Dtos.Skills;
using Mentorship.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mentorship.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController (SkillService skill) : ControllerBase
    {
        private readonly SkillService _skill = skill;

        [HttpGet]
        public async Task<IActionResult> GetSkills ()
        {
            var skills = await _skill.GetAllSkills();
            return Ok(skills);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSkill(CreateSkillDto skillDto)
        {
            var createdSkill = await _skill.CreateSkill(skillDto);
            return Ok(createdSkill);
        }
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteSkill (int id)
        {
            var skill = await _skill.DeleteSkill(id);
            return Ok(skill);
        }
    }
}
