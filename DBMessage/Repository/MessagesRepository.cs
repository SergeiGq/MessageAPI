using DBMessage.Models;
using Microsoft.EntityFrameworkCore;

namespace DBMessage.Repository;


public class MessagesRepository
{
    private readonly DbMessage _context;

    public MessagesRepository(DbMessage context)
    {
        _context = context;
    }

    public IEnumerable<Message> Get(int userId)
    {
        return _context.Messages.Include(n=>n.UserReceiver).
            Include(n=>n.UserSender)
            .Where(n=>n.UserSenderId==userId||n.UserReceiverId==userId).ToList();
    }

    public IEnumerable<Message> GetMessagesWithDefiniteUser(int userId, string nickname)
    {

        int senderId = _context.Users.FirstOrDefault(n => n.Name == nickname).Id;
        return _context.Messages.Include(n=>n.UserReceiver).
            Include(n=>n.UserSender)
            .Where(n=>(n.UserSenderId==senderId&&n.UserReceiverId==userId)||(
                n.UserSenderId==userId&&n.UserReceiverId==senderId))
            .ToList();
    }
    public void AddNewMessage(string message,int receiverId,string nickname)
    {
        int senderId = _context.Users.FirstOrDefault(n => n.Name == nickname).Id;
        _context.Messages.Add(new Message()
        {
            UserReceiverId = receiverId,
            UserSenderId = senderId,
            Text = message
        });
            
        _context.SaveChanges();
    }

    public void Clear(int userId,string nickname)
    {
        int senderId = _context.Users.FirstOrDefault(n => n.Name == nickname).Id;
        foreach (var message in _context.Messages)
        {
            if (message.UserReceiverId==senderId&&message.UserSenderId==userId)
            {
                _context.Messages.Remove(message);
            }
        }

        _context.SaveChanges();
    }

}