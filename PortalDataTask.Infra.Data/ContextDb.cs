using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace PortalDataTask.Infra.Data;

[ExcludeFromCodeCoverage]
public class ContextDb : DbContext
{
    public DbSet<Domain.Entities.DataTask>? DataTasks { get; set; }
    public DbSet<Domain.Entities.User>? Users { get; set; }

	public ContextDb(DbContextOptions<ContextDb> options) : base(options)
	{
	}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContextDb).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach(var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedAt") is not null))
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Property("CreatedAt").CurrentValue = DateTime.Now;
                    break;
            case EntityState.Modified:
                    entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
