namespace TraindingApi.WebModel;

public class ChatView
{
    public int id { get; set; }

    public string from_user { get; set; }

    public string to_user { get; set; }

    public string message { get; set; }

    public DateTime time { get; set; }
}