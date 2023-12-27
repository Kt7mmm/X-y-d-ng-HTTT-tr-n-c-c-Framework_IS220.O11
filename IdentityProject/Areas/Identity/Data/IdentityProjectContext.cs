using IdentityProject.Areas.Identity.Data;
using IdentityProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityProject.Data;

public class IdentityProjectContext : IdentityDbContext<IdentityProjectUser>
{
    //public DbSet<Customer> Customers { get; set; }

    public IdentityProjectContext(DbContextOptions<IdentityProjectContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new IdentityProjectUserEntityConfiguration());
    }
}
public class IdentityProjectUserEntityConfiguration : IEntityTypeConfiguration<IdentityProjectUser>
{
    public void Configure (EntityTypeBuilder<IdentityProjectUser> builder)
    {
       
    }
}