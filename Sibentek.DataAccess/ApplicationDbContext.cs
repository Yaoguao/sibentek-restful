using Microsoft.EntityFrameworkCore;
using Sibentek.Core.Model;

namespace Sibentek.DataAccess;

public class ApplicationDbContext : DbContext
{
    public DbSet<UserMessage> Users { get; set; }

    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=../sibentek-restful/database.db");
    }

}