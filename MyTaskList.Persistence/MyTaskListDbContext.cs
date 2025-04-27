using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MyTaskList.Application.Entities;

namespace MyTaskList.Persistence;

public class MyTaskListDbContext : DbContext
{
    public DbSet<Item> Items { get; set; }

    public MyTaskListDbContext(DbContextOptions<MyTaskListDbContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
