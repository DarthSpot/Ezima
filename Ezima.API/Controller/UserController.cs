using Ezima.API.Model;
using Ezima.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Ezima.API.Controller;

public class UserController(ILogger<ChildController> logger, UserRepository userRepository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> ListUsers()
    {
        return Ok(await userRepository.FindAll());
    }
    
    [HttpGet("/{id:int}")]
    public async Task<ActionResult<User>> GetUserById(int id)
    {
        var user = await userRepository.FindById(id);
        if (user == null)
            return BadRequest();
        return Ok(user);
    }
    //
    // [HttpPost("/login")]
    // public async Task<ActionResult<User>> Login([FromBody] LoginRequest login)
    // {
    //     
    // }
}