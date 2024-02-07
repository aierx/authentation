﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using webapi;

#nullable disable

namespace webapi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("RolePoUserPo", b =>
                {
                    b.Property<long>("RolePosId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserPosId")
                        .HasColumnType("bigint");

                    b.HasKey("RolePosId", "UserPosId");

                    b.HasIndex("UserPosId");

                    b.ToTable("t_role_user", (string)null);
                });

            modelBuilder.Entity("SpuPoSpuTypePo", b =>
                {
                    b.Property<long>("spuPosId")
                        .HasColumnType("bigint");

                    b.Property<long>("spuTypeListId")
                        .HasColumnType("bigint");

                    b.HasKey("spuPosId", "spuTypeListId");

                    b.HasIndex("spuTypeListId");

                    b.ToTable("t_spu_spu_type", (string)null);
                });

            modelBuilder.Entity("webapi.model.po.NewsPo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<string>("Creator")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifyTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<string>("attr1")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("attr2")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("attr3")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool?>("isDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("modifer")
                        .HasColumnType("longtext");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("t_news", (string)null);
                });

            modelBuilder.Entity("webapi.model.po.ResourcePo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<string>("Creator")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifyTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<int?>("Sort")
                        .HasColumnType("int");

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("contentType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("fileExtention")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("fileOrginname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("fileupName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool?>("isDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("modifer")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("Sort");

                    b.ToTable("t_resource", (string)null);
                });

            modelBuilder.Entity("webapi.model.po.RolePo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<string>("Creator")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifyTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<bool?>("isDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("modifer")
                        .HasColumnType("longtext");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("t_role", (string)null);
                });

            modelBuilder.Entity("webapi.model.po.SpuPo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<string>("Creator")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifyTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<string>("descroption")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool?>("isDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("modifer")
                        .HasColumnType("longtext");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long>("price")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("t_spu", (string)null);
                });

            modelBuilder.Entity("webapi.model.po.SpuTypePo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<string>("Creator")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifyTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<string>("attr1")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("attr2")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("attr3")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool?>("isDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("modifer")
                        .HasColumnType("longtext");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("t_spu_type", (string)null);
                });

            modelBuilder.Entity("webapi.model.po.UserPo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<string>("Creator")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifyTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<bool?>("isDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("modifer")
                        .HasColumnType("longtext");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("passwd")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<byte[]>("salt")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.HasKey("Id");

                    b.ToTable("t_user", (string)null);
                });

            modelBuilder.Entity("RolePoUserPo", b =>
                {
                    b.HasOne("webapi.model.po.RolePo", null)
                        .WithMany()
                        .HasForeignKey("RolePosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webapi.model.po.UserPo", null)
                        .WithMany()
                        .HasForeignKey("UserPosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SpuPoSpuTypePo", b =>
                {
                    b.HasOne("webapi.model.po.SpuPo", null)
                        .WithMany()
                        .HasForeignKey("spuPosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webapi.model.po.SpuTypePo", null)
                        .WithMany()
                        .HasForeignKey("spuTypeListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
