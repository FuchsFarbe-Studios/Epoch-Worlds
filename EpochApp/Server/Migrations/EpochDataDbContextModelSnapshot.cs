﻿// <auto-generated />
using System;
using EntityFrameworkCore.Jet.Metadata;
using EpochApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EpochApp.Server.Migrations
{
    [DbContext(typeof(EpochDataDbContext))]
    partial class EpochDataDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Users")
                .HasAnnotation("Jet:ValueGenerationStrategy", JetValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("EpochApp.Shared.Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Jet:ValueGenerationStrategy", JetValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("longchar");

                    b.HasKey("RoleID");

                    b.ToTable("Roles", "Users");

                    b.HasData(
                        new
                        {
                            RoleID = 1,
                            Description = "DEFAULT"
                        },
                        new
                        {
                            RoleID = 2,
                            Description = "ACTIVATED"
                        },
                        new
                        {
                            RoleID = 3,
                            Description = "MODERATOR"
                        },
                        new
                        {
                            RoleID = 4,
                            Description = "ADMIN"
                        });
                });

            modelBuilder.Entity("EpochApp.Shared.User", b =>
                {
                    b.Property<Guid>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .HasColumnType("longchar");

                    b.Property<string>("Gender")
                        .HasColumnType("longchar");

                    b.Property<string>("Password")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("UserName")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.HasKey("UserID");

                    b.ToTable("Users", "Users");
                });

            modelBuilder.Entity("EpochApp.Shared.UserRole", b =>
                {
                    b.Property<int>("RoleID")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateAssigned")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DateRemoved")
                        .HasColumnType("datetime");

                    b.HasKey("RoleID", "UserID");

                    b.HasIndex("UserID");

                    b.ToTable("UserRoles", "Users");
                });

            modelBuilder.Entity("EpochApp.Shared.UserRole", b =>
                {
                    b.HasOne("EpochApp.Shared.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EpochApp.Shared.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EpochApp.Shared.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("EpochApp.Shared.User", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
