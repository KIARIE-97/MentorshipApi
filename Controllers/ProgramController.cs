using Mentorship.Api.Dtos.Programs;
using Mentorship.Api.Entities;
using Mentorship.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mentorship.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramController(ProgramService mentorshipprogram) : ControllerBase
    {
        private readonly ProgramService _program = mentorshipprogram;

        [HttpGet]
        public async Task<IActionResult> GetPrograms()
        {
            var programs = await _program.GetAllAsync();
            return Ok(programs);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetSingleProgram(int id)
        {
            var program = await _program.GetOne(id);
            return Ok(program);
        }
        [HttpPatch("id")]
        public async Task<IActionResult> UpdateProgram(int id, [FromBody] UpdateProgramDto programDto )
        {
            var updatedProgram = await _program.UpdateMentorshipProgram(id, programDto);
            return Ok(updatedProgram);
        }
        [HttpDelete("id")]
         public async Task<IActionResult> DeleteProgram(int id)
        {
            var program = await _program.DeleteProgram(id);
            return Ok(program);
        }
    }
}
