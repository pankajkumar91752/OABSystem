
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OABSystem.Areas.Identity.Data;
using OABSystem.Models;

namespace OABSystem.Data;

public class OABSystemContext : IdentityDbContext<OABSystemUser>
{
    public OABSystemContext(DbContextOptions<OABSystemContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {


        base.OnModelCreating(builder);
        builder.Entity<HealthcareProfessional>().HasAlternateKey(e => e.Name);
        builder.Entity<HealthcareProfessional>().HasData(
            new { Name = "PAnkaj", Id = 10 },
            new { Name = "Kumar", Id = 11 }
            );
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    internal async Task SeedAsync(IServiceProvider serviceProvider)
    {
        
    }

    public DbSet<OABSystem.Models.HealthcareProfessional>? HealthcareProfessional { get; set; }
    public DbSet<OABSystem.Models.Appointment>? Appointment { get; set; }
}
