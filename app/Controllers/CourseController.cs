using Microsoft.AspNetCore.Mvc;
using TraindingApi.Data;

namespace TraindingApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CourseController : ControllerBase
{
    private readonly DbCon _conn;

    public CourseController(DbCon conn)
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