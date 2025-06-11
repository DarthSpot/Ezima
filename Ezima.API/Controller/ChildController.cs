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
    IUserInfoService userInfoService)
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
    public async Task<ActionResult<Child>> CreateChild([FromBody] ChildCreationRequest child)
    {
        var user = await userInfoService.GetUserAsync();
        var childResult = await childRepository.Save(child.ToEntity());
        if (childResult == null)
            return BadRequest();
        _logger.LogInformation("New child created: {ChildName}", child.Name);
        if (user != null)
        {
            user.Children.Add(childResult);
            await userRepository.Update(user);
        }
        return Ok(childResult);
    }

    [HttpPost("{childId:int}/activity")]
    [JWTauthorize]
    public async Task<ActionResult<Child>> AddActivity(int childId, [FromBody] int rewardActivityId)
    {
        var child = await childRepository.FindById(childId);
        var rewardActivity = await rewardActivityRepository.FindById(rewardActivityId);
        if (child == null || rewardActivity == null)
            return BadRequest();
        child.RewardActivities.Add(rewardActivity);
        await childRepository.Update(child);
        return Ok(child);
    }

    [HttpPost("{childId:int}/reward")]
    [JWTauthorize]
    public async Task<ActionResult<Child>> AddReward(int childId, [FromBody] RewardRequest reward)
    {
        RewardActivity? activity = null;
        if (reward.ActivityId != null)
        {
            activity = await rewardActivityRepository.FindById(reward.ActivityId.Value);
        }
        
        var child = await childRepository.FindById(childId);
        if (child == null)
            return BadRequest();
        var rewardEntity = reward.ToEntity();
        rewardEntity.Activity = activity;
        child.Rewards.Add(rewardEntity);
        child.RewardTime += rewardEntity.DefaultMinutes;
        await childRepository.Update(child);
        return Ok(child);
    }
    
    [HttpGet("{childId:int}/reward")]
    [JWTauthorize]
    public async Task<ActionResult<Child>> GetRewardsByChildId(int childId)
    {
        var child = await childRepository.FindById(childId);
        if (child == null)
            return BadRequest();
        return Ok(child.Rewards);
    }

    [HttpPost("{childId:int}/usage")]
    [JWTauthorize]
    public async Task<ActionResult<Child>> NoteUsage(int childId, [FromBody] RewardUsageRequest rewardUsage)
    {
        var child = await childRepository.FindById(childId);
        if (child == null)
            return BadRequest();

        var usage = rewardUsage.ToEntity();
        child.RewardUsages.Add(usage);
        child.RewardTime -= usage.Minutes;
        await childRepository.Update(child);
        return Ok(child);
    }
}