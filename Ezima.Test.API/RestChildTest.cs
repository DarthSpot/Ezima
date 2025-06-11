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

    [SetUp]
    public void Setup()
    {
        var logger = new Mock<ILogger<ChildController>>();
        var context = new EzimaTestContext();
        context.Database.EnsureCreatedAsync();
        var childRepo = new ChildRepository(context);
        var userRepo = new UserRepository(context);
        var rewardRepo = new RewardActivityRepository(context);
        var userInfoMock = Mock.Of<IUserInfoService>(i => i.GetUserAsync() == Task.FromResult((User)null));
        
            
        _controller = new ChildController(logger.Object, childRepo, rewardRepo, userRepo, userInfoMock);
    }

    [Test]
    // Are you my mommy?
    public async Task GetEmptyChild()
    {
        var result = await _controller.ListChildren();
        Assert.Multiple(() =>
        {
            Assert.That(result.Result, Is.TypeOf(typeof(OkObjectResult)));
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.Value, Is.InstanceOf(typeof(IEnumerable<Child>)));
            var children = okResult.Value as IEnumerable<Child>;
            Assert.That(children, Is.Empty);;
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
        var result = await _controller.CreateChild(request);
        Assert.Multiple(() =>
        {
            Assert.That(result.Result, Is.TypeOf(typeof(OkObjectResult)));
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.Value, Is.TypeOf(typeof(Child)));
            var child = okResult.Value as Child;
            Assert.That(child.Birthday, Is.EqualTo(request.Birthday));
            Assert.That(child.Name, Is.EqualTo(request.Name));
            Assert.That(child.RewardTime, Is.EqualTo(0));
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
        var result = await _controller.CreateChild(request);
        var child = (result.Result as OkObjectResult)?.Value as Child ?? throw new Exception();;
        var id = child.Id;
        var reward = new RewardRequest()
        {
            Comment = "Test",
            Minutes = 30
        };
        var rewardResult = await _controller.AddReward(id, reward);
        
        Assert.Multiple(() =>
        {
            Assert.That(rewardResult.Result, Is.TypeOf(typeof(OkObjectResult)));
            var rewardEntity = (rewardResult.Result as OkObjectResult).Value as Child ?? throw new Exception();
            Assert.That(rewardEntity.RewardTime, Is.EqualTo(reward.Minutes));
            Assert.That(rewardEntity.Rewards, Is.Not.Empty);
            Assert.That(rewardEntity.Rewards.Single().Comment, Is.EqualTo(reward.Comment));
            
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
        var result = await _controller.CreateChild(request);
        var child = (result.Result as OkObjectResult)?.Value as Child ?? throw new Exception();;
        var id = child.Id;
        var reward = new RewardRequest()
        {
            Comment = "Test",
            Minutes = 30
        };
        var repeats = 10;
        for (var i = 0; i < repeats; i++)
            await _controller.AddReward(id, reward);

        var userResult = await _controller.GetChildById(id);
        
        Assert.Multiple(() =>
        {
            Assert.That(userResult.Result, Is.TypeOf(typeof(OkObjectResult)));;
            var rewardEntity = (userResult.Result as OkObjectResult).Value as Child;
            Assert.That(rewardEntity.RewardTime, Is.EqualTo(reward.Minutes * repeats));;
            Assert.That(rewardEntity.Rewards.Count, Is.EqualTo(repeats));
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
        var reward = new RewardRequest()
        {
            Comment = "Test",
            Minutes = 30
        };
        var rewardResult = await _controller.AddReward(id, reward);
        var usage = new RewardUsageRequest()
        {
            Comment = "Test Usage",
            Minutes = 10
        };
        var usageResult = await _controller.NoteUsage(id, usage);
        Assert.Multiple(() =>
        {
            Assert.That(usageResult.Result, Is.TypeOf(typeof(OkObjectResult)));
            var okUsageResult = usageResult.Result as OkObjectResult;
            var child = okUsageResult.Value as Child;
            Assert.That(child.RewardTime, Is.EqualTo(reward.Minutes - usage.Minutes));
            Assert.That(child.RewardUsages, Is.Not.Empty);
            Assert.That(child.RewardUsages.Single().Comment, Is.EqualTo(usage.Comment));;
        });
    }
}

public class EzimaTestContext : EzimaContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("DataSource=file::memory:?cache=shared");
    }
}