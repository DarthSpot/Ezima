using Ezima.API.Authentication;
using Ezima.API.Model;
using Ezima.API.Repository;
using Ezima.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace Ezima.API.Controller;

[ApiController]
[Route("api/user")]
public class UserController(ILogger<ChildController> logger, 
    UserRepository userRepository,
    IAuthScopeService authScopeService) : ControllerBase
{
    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<User>>> ListUsers()
    {
        return Ok(await userRepository.FindAll());
    }
    
    [HttpGet("{id:int}")]
    [JWTauthorize]
    public async Task<ActionResult<User>> GetUserById(int id)
    {
        var userInfo = await authScopeService.GetUserAsync();
        if (userInfo?.Id != id)
            return Unauthorized();
        
        var user = await userRepository.FindById(id);
        if (user == null)
            return BadRequest();
        return Ok(user);
    }
}