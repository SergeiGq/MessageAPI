using DBMessage.Models;

namespace DBMessage.Repository;

public class UserRepository
{
    private DbMessage _context;

    public UserRepository(DbMessage context)
    {
        _context = context;
    }

    public string GetUserAuth(int userId)
    {
        return _context.Users.First(n => n.Id ==userId).Name.ToString();
        
    }

    public User? Get(string login, string password)
    {
        return _context.Users.FirstOrDefault(n => n.Login == login && n.Password == password);
    }

    public void Add(User auth)
    {
        _context.Users.Add(auth);
        _context.SaveChanges();
    }
}