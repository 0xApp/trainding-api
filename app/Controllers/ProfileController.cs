using Microsoft.AspNetCore.Mvc;
using TraindingApi.Data;
using TraindingApi.WebModel;

namespace TraindingApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfileController : ControllerBase
{
    public Repository _repo { get; }

    public ProfileController(Repository repo)
    {
        _repo = repo;
    }

    // GET
    [HttpGet]
    public async Task<ActionResult<ProfileWithTags>> Get(string id)
    {
        var profile = await _repo.GetProfileById(id);

        if (profile == null)
        {
            return NotFound();
        }

        return Ok(profile);
    }

    [HttpPost]
    public async Task<ActionResult<Profile>> Create(Profile profile)
    {
        var dbProfile = await _repo.CreateProfile(profile);
        return Ok(dbProfile);
    }
    
    [HttpPost("goal")]
    public async Task<ActionResult> SetGoal(ProfileGoalModel goal)
    {
        await _repo.SetGoal(goal.Id, goal.Goal);
        return Ok();
    }
    
    [HttpPost("tag")]
    public async Task<ActionResult> SetTag(ProfileTagModel tag)
    {
        await _repo.SetTag(tag);
        return Ok();
    }
}