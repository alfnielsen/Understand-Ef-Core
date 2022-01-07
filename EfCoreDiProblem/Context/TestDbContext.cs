using EfCoreDiProblem.Model;
using Microsoft.EntityFrameworkCore;

namespace EfCoreDiProblem.Context;

public class TestDbContext : DbContext
{

    public TestDbContext(DbContextOptions<TestDbContext> opt): base(opt)
    {
        
    }
    
    public DbSet<Item> Items { get; set; } = null!;
    public DbSet<ItemList> ItemLists { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
        base.OnConfiguring(optionsBuilder);
    }
}