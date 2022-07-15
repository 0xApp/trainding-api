using Microsoft.AspNetCore.Mvc;
using TraindingApi.Data;
using TraindingApi.WebModel;

namespace TraindingApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CourseController : ControllerBase
{
    private readonly Repository _conn;

    public CourseController(Repository conn)
    {
        _conn = conn;
    }

    // GET
    [HttpGet]
    public Task<IEnumerable<Course>> Get()
    {
        return _conn.GetCourses();
    }
    
    [HttpGet("user")]
    public Task<IEnumerable<UserCourseView>> GetUserCourse(string id)
    {
        return _conn.GetUserCourse(id);
    }
    
    [HttpPost("update")]
    public Task UpdateCourse(UserCourseModel request)
    {
        return _conn.UpdateUserCourse(request);
    }
}