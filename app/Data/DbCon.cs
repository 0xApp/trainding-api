using System.Data;
using Dapper;
using Npgsql;

namespace TraindingApi.Data;

public class DbCon : IDisposable
{
    public DbCon()
    {
        Connection = new NpgsqlConnection(ConnectionConfig.Config);
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