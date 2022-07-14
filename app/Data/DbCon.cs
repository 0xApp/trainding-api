using System.Data;
using Dapper;
using Npgsql;

namespace TraindingApi.Data;

public class DbCon : IDisposable
{
    public DbCon()
    {
        Connection = new NpgsqlConnection("Server=abul.db.elephantsql.com;Port=5432;Database=zabobdwe;User Id=zabobdwe;Password=sg2ZwRZ-5vrpRdzSQ_EwhV2tw4nkpVcw;CommandTimeout=20;");
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