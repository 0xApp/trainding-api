using System.Data;
using Dapper;
using Npgsql;

namespace TraindingApi.Data;

public class Repository : IDisposable
{
    public Repository()
    {
        Connection = new NpgsqlConnection(ConnectionConfig.Config);
    }

    private IDbConnection Connection { get; }

    public async Task<IEnumerable<Course>> GetCourses()
    {
        return  await Connection.QueryAsync<Course>("select * from courses");
        
    }

    public async Task<ProfileWithTags> GetProfileById(string id)
    {
        var profile = await Connection.QueryFirstOrDefaultAsync<ProfileWithTags>(@"SELECT p.* FROM profiles p where p.""Id"" = @id", new { id });

        if (profile != null)
        {
            profile.Tags = await Connection.QueryAsync<string>(@"select pt.tag_id from profile_tags pt where pt.profile_id = @id", new
            {
                id
            });
        }
        return profile;
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public async Task<Profile> CreateProfile(Profile profile)
    {
        await Connection.ExecuteAsync("insert into public.profiles values(@id, @name, @goal)", new
        {
            id = profile.Id,
            name = profile.Name,
            goal = profile.Goal
        });
        return profile;
    }
}