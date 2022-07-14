using System.Data;
using Dapper;
using Npgsql;

namespace TraindingApi.Data;

public class DbCon : IDisposable
{
    public DbCon()
    {
        Connection = new NpgsqlConnection("Server=10.128.10.68;Port=5432;Database=trainding;User Id=postgres;Password=password123;CommandTimeout=20;");
    }

    private IDbConnection Connection { get; }

    public async Task<IEnumerable<Course>> GetCourses()
    {
        return  await Connection.QueryAsync<Course>("select * from courses");
        
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}