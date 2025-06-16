using Ezima.API.Authentication;
using Ezima.API.Model;
using Ezima.API.Repository;
using Ezima.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace Ezima.API.Controller;

[Route("api/activity")]
[JWTauthorize]
[ApiController]
public class RewardActivityController(
    IAuthScopeService scopeService,
    ILogger<ChildController> logger,
    RewardActivityRepository rewardActivityRepository,
    ChildRepository childRepository) : EzimaControllerBase(scopeService)
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RewardActivity>>> FindAll()
    {
        return Ok(await rewardActivityRepository.FindAll());
    }

    [HttpPost]
    public async Task<ActionResult<RewardActivity>> Create([FromBody] RewardActivity rewardActivity)
    {
        var result = await rewardActivityRepository.Save(rewardActivity);
        if (result == null)
            return BadRequest();
        return Ok(result);
    }

    [HttpGet("{activityId:int}")]
    public async Task<ActionResult<RewardActivity>> GetRewardByActivityId(int activityId)
    {
        var result = await rewardActivityRepository.FindById(activityId);
        if (result == null)
            return BadRequest();
        return Ok(result);
    }

    [HttpPut("{activityId:int}")]
    public async Task<ActionResult<RewardActivity>> UpdateActivity(
        int activityId,
        [FromBody] RewardActivity? rewardActivity)
    {
        if (rewardActivity == null || rewardActivity.Id != activityId)
            return BadRequest();
        var result = await rewardActivityRepository.Update(rewardActivity);
        if (result == null)
            return BadRequest();
        return Ok(result);
    }
    
    [HttpPost("{activityId:int}/child/{childId:int}")]
    [JWTauthorize]
    public async Task<ActionResult<Child>> LinkActivity(int childId, int rewardActivityId)
    {
        var child = await childRepository.FindById(childId);
        var rewardActivity = await rewardActivityRepository.FindById(rewardActivityId);
        if (child == null || rewardActivity == null)
            return BadRequest();
        child.RewardActivities.Add(rewardActivity);
        await childRepository.Update(child);
        return Ok(child);
    }
}