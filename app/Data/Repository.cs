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

    public Task<IEnumerable<UserCourseView>> GetUserCourse(string id)
    {
        return Connection.QueryAsync<UserCourseView>(
            @"select c.*, coalesce(pc.progress, 0) as progress, coalesce(pc.state, 'NOT_STARTED') as state from courses c 
left join profile_courses pc on c.""Id"" = pc.course_id 
where exists (select 1 from course_tags ct where ct.course_id = c.""Id"" and ct.tag_id in (select pt.tag_id from profile_tags pt where pt.profile_id = @user) fetch first 1 rows only);",
            new
            {
                user = id
            });
    }

    public async Task UpdateUserCourse(UserCourseModel request)
    {
        if (request.State == "JOINED")
        {
            await Connection.ExecuteAsync("insert into profile_courses values(@user, @course, @state, 0)", new
            {
                user = request.User,
                course = request.Course,
                state = request.State,
            });
        }
        else
        {
            await Connection.ExecuteAsync("update profile_courses set state = @state where course_id = @course AND profile_id = @user", new
            {
                user = request.User,
                course = request.Course,
                state = request.State,
            });
        }
    }

    public Task<IEnumerable<ChatView>> GetChats(string from, string to)
    {
        return  Connection.QueryAsync<ChatView>("select * from chats c where (c.from_user = @from and c.to_user = @to) or (c.from_user = @to and c.to_user = @from) order by c.time desc", new
        {
            from, to
        });
    }

    public Task CreateChat(ChatModel chat)
    {
        return Connection.ExecuteAsync("insert into chats(from_user, to_user, message) values(@from, @to, @message)", new
        {
            chat.from,
            chat.to,
            chat.message
        });
    }
}