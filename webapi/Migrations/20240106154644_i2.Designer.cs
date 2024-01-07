﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using webapi;

#nullable disable

namespace webapi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240106154644_i2")]
    partial class i2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PermissionPoRolePo", b =>
                {
                    b.Property<long>("PermissionPosId")
                        .HasColumnType("bigint");

                    b.Property<long>("RolePosId")
                        .HasColumnType("bigint");

                    b.HasKey("PermissionPosId", "RolePosId");

                    b.HasIndex("RolePosId");

                    b.ToTable("t_role_permission", (string)null);
                });

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

            modelBuilder.Entity("webapi.model.po.PermissionPo", b =>
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

                    b.Property<long?>("ResourcePoId")
                        .HasColumnType("bigint");

                    b.Property<string>("modifer")
                        .HasColumnType("longtext");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ResourcePoId");

                    b.ToTable("t_permission", (string)null);
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

                    b.Property<string>("modifer")
                        .HasColumnType("longtext");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("type")
                        .HasColumnType("int");

                    b.HasKey("Id");

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

                    b.Property<long?>("ResourcePoId")
                        .HasColumnType("bigint");

                    b.Property<string>("modifer")
                        .HasColumnType("longtext");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ResourcePoId");

                    b.ToTable("t_role", (string)null);
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

                    b.Property<string>("modifer")
                        .HasColumnType("longtext");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("t_user", (string)null);
                });

            modelBuilder.Entity("PermissionPoRolePo", b =>
                {
                    b.HasOne("webapi.model.po.PermissionPo", null)
                        .WithMany()
                        .HasForeignKey("PermissionPosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webapi.model.po.RolePo", null)
                        .WithMany()
                        .HasForeignKey("RolePosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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

            modelBuilder.Entity("webapi.model.po.PermissionPo", b =>
                {
                    b.HasOne("webapi.model.po.ResourcePo", null)
                        .WithMany("PermissionPos")
                        .HasForeignKey("ResourcePoId");
                });

            modelBuilder.Entity("webapi.model.po.RolePo", b =>
                {
                    b.HasOne("webapi.model.po.ResourcePo", null)
                        .WithMany("RolePos")
                        .HasForeignKey("ResourcePoId");
                });

            modelBuilder.Entity("webapi.model.po.ResourcePo", b =>
                {
                    b.Navigation("PermissionPos");

                    b.Navigation("RolePos");
                });
#pragma warning restore 612, 618
        }
    }
}
