namespace TraindingApi.Data;

public class ProfileWithTags : Profile
{
    public IEnumerable<string> Tags { get; set; }
}