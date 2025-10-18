using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess;

public class CashFlowDBContext : DbContext
{
    public CashFlowDBContext(DbContextOptions options) : base(options) {}
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Tag>().ToTable("Tags"); // muda a tabela Tag para Tags
    }
}
