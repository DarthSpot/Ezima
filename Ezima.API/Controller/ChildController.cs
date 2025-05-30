using Ezima.API.Authentication;
using Ezima.API.Model;
using Ezima.API.Model.Request;
using Ezima.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Ezima.API.Controller;

[Route("api/child")]
[Jauthorize]
[ApiController]
public class ChildController(ILogger<ChildController> logger, ChildRepository childRepository, RewardActivityRepository rewardActivityRepository)
    : ControllerBase
{
    private readonly ILogger<ChildController> _logger = logger;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Child>>> ListChildren()
    {
        return Ok(await childRepository.FindAll());
    }

    [HttpGet("{childId:int}")]
    public async Task<ActionResult<Child>> GetChildById(int childId)
    {
        var child = await childRepository.FindById(childId);
        if (child == null)
            return BadRequest();
        return Ok(child);
    }

    [HttpPost]
    public async Task<ActionResult<Child>> CreateChild([FromBody] ChildCreationRequest child)
    {
        var childResult = await childRepository.Save(child.ToEntity());
        if (childResult == null)
            return BadRequest();
        return Ok(childResult);
    }

    [HttpPost("{childId:int}/activity")]
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
    public async Task<ActionResult<Child>> GetRewardsByChildId(int childId)
    {
        var child = await childRepository.FindById(childId);
        if (child == null)
            return BadRequest();
        return Ok(child.Rewards);
    }

    [HttpPost("{childId:int}/usage")]
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