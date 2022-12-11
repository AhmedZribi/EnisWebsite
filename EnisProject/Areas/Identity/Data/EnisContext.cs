using EnisProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EnisProject.Data;

public class EnisContext : IdentityDbContext<ApplicationUser>
{
    public EnisContext(DbContextOptions<EnisContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ApplicationUser>().ToTable("Users", "security");
        builder.Entity<IdentityRole>().ToTable("Roles", "security");
        builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
        builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
        builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
        builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");
    }

    public DbSet<EnisProject.Models.Internship> Internship { get; set; }

    public DbSet<EnisProject.Models.Speciality> Speciality { get; set; }

    public DbSet<EnisProject.Models.Class> Class { get; set; }

}
