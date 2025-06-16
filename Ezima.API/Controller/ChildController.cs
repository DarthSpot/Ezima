using Ezima.API.Authentication;
using Ezima.API.Model;
using Ezima.API.Model.Request;
using Ezima.API.Repository;
using Ezima.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace Ezima.API.Controller;

[Route("api/child")]
[JWTauthorize]
[ApiController]
public class ChildController(
    ILogger<ChildController> logger,
    ChildRepository childRepository,
    RewardActivityRepository rewardActivityRepository,
    UserRepository userRepository,
    IAuthScopeService authScopeService)
    : ControllerBase
{
    private readonly ILogger<ChildController> _logger = logger;

    [HttpGet]
    [JWTauthorize]
    public async Task<ActionResult<IEnumerable<Child>>> ListChildren()
    {
        return Ok(await childRepository.FindAll());
    }

    [HttpGet("{childId:int}")]
    [JWTauthorize]
    public async Task<ActionResult<Child>> GetChildById(int childId)
    {
        var child = await childRepository.FindById(childId);
        if (child == null)
            return BadRequest();
        return Ok(child);
    }

    [HttpPost]
    [JWTauthorize]
    public async Task<ActionResult<Child>> CreateChild([FromBody] ChildCreationRequest childRequest)
    {
        var user = await authScopeService.GetUserAsync();
        var child = childRequest.ToEntity();
        child.Parents.Add(user);
        var childResult = await childRepository.Save(child);
        
        if (childResult == null)
            return BadRequest();
        _logger.LogInformation("New child created: {ChildName}", child.Name);
        return Ok(childResult);
    }
}