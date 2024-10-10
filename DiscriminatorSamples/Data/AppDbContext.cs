using DiscriminatorSamples.Business;
using DiscriminatorSamples.Business.Interfaces;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    private string _authenticatedUser;

    public AppDbContext(DbContextOptions options, IAuthenticatedUserService authenticatedUserService) : base(options) 
    {
        _authenticatedUser = authenticatedUserService.GetAuthenticatedUserOrDefault();
    }

    public DbSet<ContractEmployee> ContractEmployees { get; set; }
    public DbSet<RegisteredEmployee> RegisteredEmployees { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var modelEntityTypes = modelBuilder.Model.GetEntityTypes();

        foreach (var entity in modelEntityTypes)
        {
            if (entity.ClrType.IsSubclassOf(typeof(BaseEntity)))
            {
                modelBuilder.Entity(entity.Name)
                    .Property<Guid>("Id").HasColumnName("Id");
            }
        }
        
        foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        {
            property.SetColumnType("decimal(18,6)");
        }

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        modelBuilder.Entity<Employee>()
        .ToTable("Employees")
        .HasDiscriminator<EmployeeType>("Type")
        .HasValue<ContractEmployee>(EmployeeType.Contract)
        .HasValue<RegisteredEmployee>(EmployeeType.Registered);

        modelBuilder.Entity<Employee>()
        .Property(c => c.Name).HasColumnType("varchar(500)");

        modelBuilder.Entity<Employee>()
            .Property(c => c.Type)
            .HasColumnType("varchar(500)");

        modelBuilder.Entity<ContractEmployee>()
            .Property(c => c.Contract).HasColumnType("varchar(100)");

        modelBuilder.Entity<RegisteredEmployee>()
            .Property(c => c.RegistrationNumber).HasColumnType("varchar(100)");

        modelBuilder.Entity<Customer>()
            .Property(c => c.Name)
            .HasColumnType("varchar(500)");

        foreach (var entityType in modelEntityTypes)
        {
            if (entityType.ClrType.IsSubclassOf(typeof(AuditableBaseEntity)))
            {
                modelBuilder.Entity(entityType.Name).Property<string>("CreatedBy").HasColumnType("nvarchar(500)");
                modelBuilder.Entity(entityType.Name).Property<string>("LastModifiedBy").HasColumnType("nvarchar(500)");
                modelBuilder.Entity(entityType.Name).Property<DateTime>("CreatedAt");
                modelBuilder.Entity(entityType.Name).Property<DateTime?>("LastModifiedAt");
            }
        }

        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        var timestamp = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = timestamp;
                    entry.Entity.LastModifiedAt = timestamp;
                    entry.Entity.CreatedBy = _authenticatedUser;
                    entry.Entity.LastModifiedBy = _authenticatedUser;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedAt = timestamp;
                    entry.Entity.LastModifiedBy = _authenticatedUser;
                    break;
            }
        }

        return base.SaveChanges();
    }
}
