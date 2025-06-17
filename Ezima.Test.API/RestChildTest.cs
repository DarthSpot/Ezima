using Ezima.API.Controller;
using Ezima.API.Model;
using Ezima.API.Model.Context;
using Ezima.API.Model.Request;
using Ezima.API.Repository;
using Ezima.API.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Ezima.Test.API;

public class RestChildTest
{
    private ChildController _controller;
    private RewardController _rewardController;
    private UsageController _usageController;

    [SetUp]
    public async Task Setup()
    {
        var logger = new Mock<ILogger<ChildController>>();
        var context = new EzimaTestContext();
        await context.Database.EnsureCreatedAsync();
        var childRepo = new ChildRepository(context);
        var userRepo = new UserRepository(context);

        var user = new User()
        {
            Id = 1,
            EMail = "None",
            FullName = "TestUser",
            Username = "TestUser",
            OAuthId = "None"
        };
        if ((await userRepo.FindById(user.Id)) == null)
            await userRepo.Save(user);
        var rewardActivityRepository = new RewardActivityRepository(context);
        var rewardRepository = new RewardRepository(context);
        var usageRepository = new UsageRepository(context);
        var userScopeService = new EzimaTestScopeService(user.Id, userRepo);
            
        _controller = new ChildController(userScopeService, logger.Object, childRepo);
        _rewardController = new RewardController(userScopeService, rewardActivityRepository, rewardRepository, childRepo);
        _usageController = new UsageController(userScopeService, usageRepository, childRepo);
    }

    private async Task<T?> Unwrap<T>(Func<Task<ActionResult<T>>> call)
    {
        var result = await call();
        if (result.Result is not OkObjectResult okResult) 
            return default;
        if (okResult.Value == null)
            return default;
        return (T)okResult.Value;
    }

    [Test]
    // Are you my mommy?
    public async Task GetEmptyChild()
    {
        var result = await Unwrap(_controller.ListChildren);
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Empty);;
        });
    }
    
    [Test]
    public async Task TestCreateChild()
    {
        var request = new ChildCreationRequest()
        {
            Birthday = DateTime.Today,
            Name = "Test"
        };
        var child = await Unwrap(() => _controller.CreateChild(request));
        Assert.Multiple(() =>
        {
            Assert.That(child, Is.Not.Null);
            Assert.That(child.Birthday, Is.EqualTo(request.Birthday));
            Assert.That(child.Name, Is.EqualTo(request.Name));
            Assert.That(child.Rewards, Is.Empty);
            Assert.That(child.RewardActivities, Is.Empty);
        });
    }

    [Test]
    public async Task TestReward()
    {
        var request = new ChildCreationRequest()
        {
            Birthday = DateTime.Today,
            Name = "Test"
        };
        var child = await Unwrap(() => _controller.CreateChild(request));
        var id = child.Id;
        var reward = new RewardRequest()
        {
            Comment = "Test",
            Minutes = 30,
            ChildrenIds = {id}
        };
        var rewardResult = await Unwrap(() => _rewardController.AddReward(reward));
        var rewardList = await Unwrap(() => _rewardController.GetRewards());
        
        Assert.Multiple(() =>
        {
            Assert.That(rewardList, Is.Not.Empty);
            Assert.That(rewardList.Count, Is.EqualTo(1));
            Assert.That(rewardList.Single().Comment, Is.EqualTo(reward.Comment));
            
        });
    }
    
    [Test]
    public async Task TestMultiReward()
    {
        var request = new ChildCreationRequest()
        {
            Birthday = DateTime.Today,
            Name = "Test"
        };
        var child = await Unwrap(() => _controller.CreateChild(request));
        var id = child.Id;
        var reward = new RewardRequest()
        {
            Comment = "Test",
            Minutes = 30,
            ChildrenIds = {id}
        };
        var repeats = 10;
        for (var i = 0; i < repeats; i++)
        {
            var result = await Unwrap(() => _rewardController.AddReward(reward));
            Assert.That(result, Is.Not.Null);
        }

        var userResult = await Unwrap(() => _controller.GetChildById(id));
        var rewardTimeResult = await Unwrap(() => _rewardController.GetTotalRewardTime(id));
        Assert.Multiple(() =>
        {
            Assert.That(userResult.Rewards.Count, Is.EqualTo(repeats));
            Assert.That(rewardTimeResult, Is.EqualTo(repeats * reward.Minutes));
        });
    }
    
    [Test]
    public async Task TestUsage()
    {
        var request = new ChildCreationRequest()
        {
            Birthday = DateTime.Today,
            Name = "Test"
        };
        var result = await _controller.CreateChild(request);
        var child = (result.Result as OkObjectResult)?.Value as Child ?? throw new Exception();;
        var id = child.Id;
        var usage = new RewardUsageRequest()
        {
            Comment = "Test Usage",
            Minutes = 10,
            ChildrenIds = {id}
        };
        var usageResult = await _usageController.AddNewUsage(usage);
        var usages = await Unwrap(_usageController.GetAllUsages);
        var usageTotal = await Unwrap(_usageController.GetTotal);
        Assert.Multiple(() =>
        {
            Assert.That(usages, Is.Not.Empty);
            Assert.That(usages.Single().Comment, Is.EqualTo(usage.Comment));
        });
    }
    
    [Test]
    public async Task TestMultiUsage()
    {
        var request = new ChildCreationRequest()
        {
            Birthday = DateTime.Today,
            Name = "Test"
        };
        var child = await Unwrap(() => _controller.CreateChild(request));
        var id = child.Id;
        var usage = new RewardUsageRequest()
        {
            Comment = "Test Usage",
            Minutes = 10,
            ChildrenIds = {id}
        };
        var repeats = 10;
        for (var i = 0; i < repeats; i++)
        {
            var result = await Unwrap(() => _usageController.AddNewUsage(usage));
            Assert.That(result, Is.Not.Null);
        }

        var userResult = await Unwrap(() => _controller.GetChildById(id));
        var rewardTimeResult = await Unwrap(() => _usageController.GetTotalUsageForChild(id));
        Assert.Multiple(() =>
        {
            Assert.That(userResult.RewardUsages.Count, Is.EqualTo(repeats));
            Assert.That(rewardTimeResult, Is.EqualTo(repeats * usage.Minutes));
        });
    }
}

public class EzimaTestScopeService : IAuthScopeService
{
    private readonly int _defaultUserId;
    private readonly UserRepository _userRepository;

    public EzimaTestScopeService(int defaultUserId, UserRepository userRepository)
    {
        _defaultUserId = defaultUserId;
        _userRepository = userRepository;
    }

    public async Task<User> GetUserAsync()
    {
        return await _userRepository.FindById(_defaultUserId);
    }

    public async Task<IUserScope> GetUserScopeAsync()
    {
        return new ApiUserScope(await GetUserAsync());
    }
}

public class EzimaTestContext : EzimaContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("DataSource=file::memory:?cache=shared");
    }
}