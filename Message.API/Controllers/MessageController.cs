using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using DBMessage.Models;
using DBMessage.Repository;
using Microsoft.AspNetCore.Authorization;

namespace Message.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{
    private readonly ILogger<MessageController> _logger;
    private readonly MessagesRepository _repository;

    public MessageController(ILogger<MessageController> logger, MessagesRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }
    [Authorize]
    [HttpGet]
    public IEnumerable<DBMessage.Models.Message> Get()
    {
        return _repository.Get(User.GetId());
    }
    
    [Authorize]
    [HttpPost("AddNewMessage")]
    public void Post(string message, string nickname)
    {
        _repository.AddNewMessage(message, User.GetId(), nickname);
    }
    [Authorize]
    [HttpDelete("RemoveMessage")]
    public void ClearAllMessageWithUser(string nickname)
    {
        _repository.Clear(User.GetId(),nickname);
    }

    
}