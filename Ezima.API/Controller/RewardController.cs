using Ezima.API.Authentication;
using Ezima.API.Model;
using Ezima.API.Model.Request;
using Ezima.API.Repository;
using Ezima.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace Ezima.API.Controller;

[Route("api/reward")]
[JWTauthorize]
[ApiController]
public class RewardController(
    IAuthScopeService scopeService,
    RewardActivityRepository rewardActivityRepository,
    RewardRepository rewardRepository,
    ChildRepository childRepository
    ) : EzimaControllerBase(scopeService)
{
    [HttpGet]
    [JWTauthorize]
    public async Task<ActionResult<List<Reward>>> GetRewards()
    {
        var user = await scopeService.GetUserAsync();
        var children = await childRepository.FindAllByUserId(user.Id);
        return Ok(children.SelectMany(x => x.Rewards).ToList());
    }
    
    [HttpPost]
    [JWTauthorize]
    public async Task<ActionResult<Reward>> AddReward([FromBody] RewardRequest rewardRequest)
    {
        var scope = await scopeService.GetUserScopeAsync();
        var reward = rewardRequest.ToEntity();
        if (rewardRequest.ActivityId != null)
        {
            reward.Activity = await rewardActivityRepository.FindById(rewardRequest.ActivityId.Value);
        }
        foreach (var childId in rewardRequest.ChildrenIds)
        {
            var child = await childRepository.FindById(childId);
            if (child == null)
                return BadRequest();
            reward.Children.Add(child);
        }
        if (!scope.HasAccessToData(reward))
            return Unauthorized();

        var result = await rewardRepository.Save(reward);
        if (result == null)
            return BadRequest();
        return Ok(result);
    }

    [HttpGet("{rewardId:int}")]
    [JWTauthorize]
    public async Task<ActionResult<Reward>> GetReward(int rewardId)
    {
        var scope = await scopeService.GetUserScopeAsync();
        var reward = await rewardRepository.FindById(rewardId);
        if (reward == null)
            return NotFound();
        if (!scope.HasAccessToData(reward))
            return Unauthorized();
        return Ok(reward);
    }
    
    [HttpGet("child/{childId:int}")]
    [JWTauthorize]
    public async Task<ActionResult<List<Reward>>> GetRewardsByChildId(int childId)
    {
        var scope = await scopeService.GetUserScopeAsync();
        if (!scope.HasAccessToChild(childId))
            return Unauthorized();
        var child = await childRepository.FindById(childId);
        if (child == null)
            return BadRequest();
        return Ok(child.Rewards);
    }
    
    [HttpGet("child/{childId:int}/total")]
    [JWTauthorize]
    public async Task<ActionResult<int>> GetTotalRewardTime(int childId)
    {
        var scope = await scopeService.GetUserScopeAsync();
        if (!scope.HasAccessToChild(childId))
            return Unauthorized();
        var child = await childRepository.FindById(childId);
        if (child == null)
            return BadRequest();
        var rewards = child.Rewards.Select(x => x.Minutes).Sum();
        if (child == null)
            return BadRequest();
        return Ok(rewards);
    }
}