using Mentorship.Api.Dtos.Users;
using Mentorship.Api.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mentorship.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController (UserRepository repo) : ControllerBase
    {
        private readonly UserRepository _repo = repo;
         [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetAll();
            return Ok(users);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetSingleUser(int id)
        {
            var user = await _repo.GetSingle(id);
            return Ok(user);
        }
        [HttpPatch("id")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto userDto )
        {
            var updateduser = await _repo.UpdateUser(id, userDto);
            return Ok(updateduser);
        }
        [HttpDelete("id")]
         public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _repo.DeleteUser(id);
            return Ok(user);
        }
    }
}
