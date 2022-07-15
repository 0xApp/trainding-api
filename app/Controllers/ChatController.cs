using Microsoft.AspNetCore.Mvc;
using TraindingApi.Data;
using TraindingApi.WebModel;

namespace TraindingApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ChatController : ControllerBase
{
    private readonly Repository _repository;

    public ChatController(Repository repository)
    {
        _repository = repository;
    }

    // GET
    [HttpGet]
    public Task<IEnumerable<ChatView>> Get(string from, string to)
    {
        return _repository.GetChats(from, to);
    }
    
    [HttpPost]
    public Task Create(ChatModel chat)
    {
        return _repository.CreateChat(chat);
    }
}