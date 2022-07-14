using System.Data;
using Dapper;
using Npgsql;
using TraindingApi.WebModel;

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
        Connection.Dispose();
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

    public async Task SetGoal(string user, int goal)
    {
        await Connection.ExecuteAsync(@"update profiles set ""Goal""= @goal where ""Id"" = @user ", new
        {
            user, goal
        });
    }

    public async Task SetTag(ProfileTagModel tag)
    {
        if (tag.IsSet)
        {
            await Connection.ExecuteAsync(@"insert into profile_tags values(@user, @tag)", new
            {
                user = tag.User,
                tag = tag.Tag
            });
        }
        else
        {
            await Connection.ExecuteAsync(@"delete from profile_tags where profile_id = @user and tag_id = @tag", new
            {
                user = tag.User,
                tag = tag.Tag
            });
        }
    }
}