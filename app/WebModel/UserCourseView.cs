namespace TraindingApi.WebModel;

public class UserCourseView
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Source { get; set; }

    public float Rating { get; set; }

    public int UserCount { get; set; }

    public int Duration { get; set; }

    public int progress { get; set; }
    
    public string state { get; set; }
}