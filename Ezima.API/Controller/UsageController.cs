using Ezima.API.Authentication;
using Ezima.API.Model;
using Ezima.API.Model.Request;
using Ezima.API.Repository;
using Ezima.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace Ezima.API.Controller;

[Route("api/usage")]
[JWTauthorize]
[ApiController]
public class UsageController(IAuthScopeService scopeService,
    UsageRepository usageRepository,
    ChildRepository childRepository) : EzimaControllerBase(scopeService)
{
    [HttpGet()]
    [JWTauthorize]
    public async Task<ActionResult<List<RewardUsage>>> GetAllUsages()
    {
        var scope = await scopeService.GetUserScopeAsync();
        var children = await childRepository.FindAllByUserId(scope.UserId);
        var result = children.SelectMany(x => x.RewardUsages).Distinct().ToList();
        return Ok(result);
    }
    
    [HttpGet("{usageId:int}")]
    [JWTauthorize]
    public async Task<ActionResult<RewardUsage>> GetUsageById(int usageId)
    {
        var user = await scopeService.GetUserScopeAsync();
        var usage = await usageRepository.FindById(usageId);
        if (usage == null)
        {
            return NotFound();
        }
        if (!user.HasAccessToData(usage))
            return BadRequest();
        return Ok(usage);
    }
    
    [HttpGet("child/{childId:int}")]
    [JWTauthorize]
    public async Task<ActionResult<List<RewardUsage>>> GetAllUsagesForChild(int childId)
    {
        var user = await scopeService.GetUserScopeAsync();
        if (!user.HasAccessToChild(childId))
            return BadRequest();
        var child = await childRepository.FindById(childId);
        if (child == null)
            return NotFound();
        return Ok(child.RewardUsages);
    }
    
    [HttpGet("child/{childId:int}/total")]
    [JWTauthorize]
    public async Task<ActionResult<List<RewardUsage>>> GetTotalUsageForChild(int childId)
    {
        var user = await scopeService.GetUserScopeAsync();
        if (!user.HasAccessToChild(childId))
            return BadRequest();
        var child = await childRepository.FindById(childId);
        if (child == null)
            return NotFound();
        return Ok(child.RewardUsages.Sum(x => x.Minutes));
    }

    [HttpPost()]
    [JWTauthorize]
    public async Task<ActionResult<RewardUsage>> AddNewUsage([FromBody] RewardUsageRequest usageRequest)
    {
        var user = await scopeService.GetUserScopeAsync();
        if (usageRequest.ChildrenIds.Any(x => !user.HasAccessToChild(x)))
            return BadRequest();
        var usage = usageRequest.ToEntity();
        foreach (var childId in usageRequest.ChildrenIds)
        {
            var child = await childRepository.FindById(childId);
            if (child == null)
                return BadRequest();
            usage.Children.Add(child);
        }
        var saveResult = await usageRepository.Save(usage);
        if (saveResult == null)
            return BadRequest();
        return Ok(saveResult);
    }

    [HttpGet("total")]
    [JWTauthorize]
    public async Task<ActionResult<int>> GetTotal()
    {
        var user = await scopeService.GetUserScopeAsync();
        var children = await childRepository.FindAllByUserId(user.UserId);
        return Ok(children.SelectMany(x => x.RewardUsages).Sum(x => x.Minutes));
    }
    
    
}