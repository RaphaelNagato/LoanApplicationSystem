using Core.Entities;
using Infrastructure.DB.Config;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DB;

internal class LoanDbContext(DbContextOptions<LoanDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new LoanApplicationConfiguration());
    }

    public DbSet<LoanApplication> LoanApplications { get; set; }
}