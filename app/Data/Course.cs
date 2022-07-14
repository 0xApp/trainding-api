namespace TraindingApi.Data;

public class Course
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Source { get; set; }

    public float Rating { get; set; }

    public int UserCount { get; set; }

    public int Duration { get; set; }
}