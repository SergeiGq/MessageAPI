using DBMessage.Models;
using Microsoft.EntityFrameworkCore;

namespace DBMessage;

public class DbMessage:DbContext
{
    private bool _initialized;

    public DbMessage()
    {
        
    }

    public DbMessage(DbContextOptions options) : base(options)
    {
        _initialized = true;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!_initialized)
        {
            optionsBuilder.UseNpgsql();
        }
    }

    public DbSet<User> Users{ get; set; }= null!;
    public DbSet<Message> Messages{ get; set; }= null!;
    



}