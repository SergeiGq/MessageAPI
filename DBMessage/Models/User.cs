using Microsoft.EntityFrameworkCore;

namespace DBMessage.Models;

[Index(nameof(Login),IsUnique = true)]
[Index(nameof(Name),IsUnique = true)]
public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Login { get; set; }= null!;
    public string Password { get; set; }= null!;
    
    
}