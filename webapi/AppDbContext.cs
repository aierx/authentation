using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
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
    public DbSet<NewsPo> News { set; get; }
    public DbSet<SpuPo> Spu { set; get; }
    public DbSet<SpuTypePo> SpuType { set; get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var jsonConverter =new ValueConverter<Dictionary<string, string>, string>(v => JsonConvert.SerializeObject(v),
            v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v)!);
        var valueComparer = new ValueComparer<Dictionary<string, string>>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c
            );
        modelBuilder.Entity<UserPo>().ToTable("t_user");
        modelBuilder.Entity<RolePo>().ToTable("t_role");
        modelBuilder.Entity<ResourcePo>().ToTable("t_resource");
        
        modelBuilder.Entity<SpuPo>().ToTable("t_spu");
        modelBuilder.Entity<SpuPo>().Property(e=>e.name).HasConversion(jsonConverter).Metadata.SetValueComparer(valueComparer);

        modelBuilder.Entity<SpuPo>().Property(e=>e.descroption).HasConversion(jsonConverter).Metadata.SetValueComparer(valueComparer);
        modelBuilder.Entity<SpuTypePo>().ToTable("t_spu_type");
        
        modelBuilder.Entity<NewsPo>().ToTable("t_news");
        modelBuilder.Entity<NewsPo>().Property(e=>e.title).HasConversion(jsonConverter).Metadata.SetValueComparer(valueComparer);
        modelBuilder.Entity<NewsPo>().Property(e=>e.description).HasConversion(jsonConverter).Metadata.SetValueComparer(valueComparer);
        modelBuilder.Entity<NewsPo>().Property(e=>e.content).HasConversion(jsonConverter).Metadata.SetValueComparer(valueComparer);



        modelBuilder.Entity<UserPo>().Property(e => e.CreateTime).ValueGeneratedOnAdd().HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        modelBuilder.Entity<UserPo>().Property(e => e.ModifyTime).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        
        modelBuilder.Entity<RolePo>().Property(e => e.CreateTime).ValueGeneratedOnAdd().HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        modelBuilder.Entity<RolePo>().Property(e => e.ModifyTime).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

        modelBuilder.Entity<ResourcePo>().Property(e => e.CreateTime).ValueGeneratedOnAdd().HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        modelBuilder.Entity<ResourcePo>().Property(e => e.ModifyTime).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        
        modelBuilder.Entity<SpuPo>().Property(e => e.CreateTime).ValueGeneratedOnAdd().HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        modelBuilder.Entity<SpuPo>().Property(e => e.ModifyTime).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        
        modelBuilder.Entity<SpuTypePo>().Property(e => e.CreateTime).ValueGeneratedOnAdd().HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        modelBuilder.Entity<SpuTypePo>().Property(e => e.ModifyTime).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        
        modelBuilder.Entity<NewsPo>().Property(e => e.CreateTime).ValueGeneratedOnAdd().HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        modelBuilder.Entity<NewsPo>().Property(e => e.ModifyTime).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        

        modelBuilder.Entity<RolePo>().HasMany<UserPo>(e => e.UserPos).WithMany(e => e.RolePos).UsingEntity(e => e.ToTable("t_role_user"));

        modelBuilder.Entity<SpuPo>().HasMany<SpuTypePo>(e => e.spuTypeList).WithMany(e => e.spuPos).UsingEntity(e => e.ToTable("t_spu_spu_type"));
    }
}