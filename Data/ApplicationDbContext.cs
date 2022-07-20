using ChealCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChealCore.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    //public DbSet<ApplicationRole>? ApplicationRole { get; set; }

    public DbSet<GLCategory>? GLCategory { get; set; }

    public DbSet<Branch>? Branch { get; set; }

    public DbSet<GLAccount>? GLAccount { get; set; }

    public DbSet<UserTill>? UserTill { get; set; }

    public DbSet<AccountConfiguration>? AccountConfiguration { get; set; }

    public DbSet<GLPosting>? GLPosting { get; set; }

    public DbSet<Customer>? Customer { get; set; }

    public DbSet<CustomerAccount>? CustomerAccount { get; set; }

    public DbSet<TellerPosting>? TellerPosting { get; set; }

    public DbSet<Transaction>? Transaction { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("Identity");
        builder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable(name: "User");
        });
        builder.Entity<IdentityRole>(entity =>
        {
            entity.ToTable(name: "Role");
        });
        builder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.ToTable("UserRoles");
        });
        builder.Entity<IdentityUserClaim<string>>(entity =>
        {
            entity.ToTable("UserClaims");
        });
        builder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.ToTable("UserLogins");
        });
        builder.Entity<IdentityRoleClaim<string>>(entity =>
        {
            entity.ToTable("RoleClaims");
        });
        builder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.ToTable("UserTokens");
        });
    }
}

