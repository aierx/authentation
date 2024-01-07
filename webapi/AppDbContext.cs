using Microsoft.EntityFrameworkCore;
using webapi.model.po;

namespace webapi;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ResourcePo> Resource { set; get; }
    public DbSet<RolePo> Role { set; get; }
    public DbSet<UserPo> User { set; get; }
    public DbSet<PermissionPo> Permission { set; get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ResourcePo>().ToTable("t_resource");
        modelBuilder.Entity<RolePo>().ToTable("t_role");
        modelBuilder.Entity<UserPo>().ToTable("t_user");
        modelBuilder.Entity<PermissionPo>().ToTable("t_permission");

        modelBuilder.Entity<ResourcePo>().Property(e => e.CreateTime).ValueGeneratedOnAdd()
            .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        modelBuilder.Entity<ResourcePo>().Property(e => e.ModifyTime).ValueGeneratedOnAddOrUpdate()
            .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

        modelBuilder.Entity<RolePo>().Property(e => e.CreateTime).ValueGeneratedOnAdd()
            .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        modelBuilder.Entity<RolePo>().Property(e => e.ModifyTime).ValueGeneratedOnAddOrUpdate()
            .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

        modelBuilder.Entity<UserPo>().Property(e => e.CreateTime).ValueGeneratedOnAdd()
            .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        modelBuilder.Entity<UserPo>().Property(e => e.ModifyTime).ValueGeneratedOnAddOrUpdate()
            .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

        modelBuilder.Entity<PermissionPo>().Property(e => e.CreateTime).ValueGeneratedOnAdd()
            .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        modelBuilder.Entity<PermissionPo>().Property(e => e.ModifyTime).ValueGeneratedOnAddOrUpdate()
            .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

        modelBuilder.Entity<ResourcePo>().HasMany<PermissionPo>(e => e.PermissionPos).WithOne();
        modelBuilder.Entity<ResourcePo>().HasMany<RolePo>(e => e.RolePos).WithOne();
        modelBuilder.Entity<RolePo>().HasMany<UserPo>(e => e.UserPos).WithMany(e => e.RolePos)
            .UsingEntity(e => e.ToTable("t_role_user"));
        modelBuilder.Entity<RolePo>().HasMany<PermissionPo>(e => e.PermissionPos).WithMany(e => e.RolePos)
            .UsingEntity(e => e.ToTable("t_role_permission"));
    }
}