using System.ComponentModel.DataAnnotations.Schema;

namespace DBMessage.Models;

public class Message
{
    public int Id  { get; set; }
    [ForeignKey(nameof(UserSenderId))]
    public User UserSender { get; set; }
    [ForeignKey(nameof(UserReceiverId))]
    public User UserReceiver { get; set; }
    public string Text { get; set; }
    public int UserSenderId { get; set; }
    public int UserReceiverId { get; set; }
    
    
}