using Microsoft.AspNetCore.Mvc;
using TraindingApi.Data;

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
}