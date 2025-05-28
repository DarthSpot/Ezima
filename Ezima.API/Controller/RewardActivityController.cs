using Ezima.API.Model;
using Ezima.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Ezima.API.Controller;

[Route("api/activity")]
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
}