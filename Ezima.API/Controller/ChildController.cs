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
    IAuthScopeService authScopeService,
    ILogger<ChildController> logger,
    ChildRepository childRepository)
    : ControllerBase
{
    [HttpGet]
    [JWTauthorize]
    public async Task<ActionResult<IEnumerable<Child>>> ListChildren()
    {
        var user = await authScopeService.GetUserAsync();
        return Ok(await childRepository.FindAllByUserId(user.Id));
    }

    [HttpGet("infos")]
    [JWTauthorize]
    public async Task<ActionResult<IEnumerable<ChildInfo>>> ListChildInfos()
    {
        var user = await authScopeService.GetUserScopeAsync();
        var children = await childRepository.FindAllByUserId(user.UserId);
        var infos = children.Select(x => new ChildInfo()
        {
            Id = x.Id,
            Name = x.Name,
            CurrentRewardTime = x.Rewards.Sum(r => r.Minutes) - x.RewardUsages.Sum(u => u.Minutes)
        }).ToList();
        return Ok(infos);
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
        logger.LogInformation("New child created: {ChildName}", child.Name);
        return Ok(childResult);
    }
}