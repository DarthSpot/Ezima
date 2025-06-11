using Ezima.API.Authentication;
using Ezima.API.Model;
using Ezima.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Ezima.API.Controller;

[Route("api/activity")]
[JWTauthorize]
[ApiController]
public class RewardActivityController(ILogger<ChildController> logger, RewardActivityRepository rewardActivityRepository) : ControllerBase
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
}