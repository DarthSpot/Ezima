using Ezima.API.Model;
using Ezima.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Ezima.API.Controller;

[ApiController]
[Route("api/user")]
public class UserController(ILogger<ChildController> logger, UserRepository userRepository) : ControllerBase
{
    [HttpGet("[controller]")]
    public async Task<ActionResult<IEnumerable<User>>> ListUsers()
    {
        return Ok(await userRepository.FindAll());
    }
    
    [HttpGet("[controller]/{id:int}")]
    public async Task<ActionResult<User>> GetUserById(int id)
    {
        var user = await userRepository.FindById(id);
        if (user == null)
            return BadRequest();
        return Ok(user);
    }
}