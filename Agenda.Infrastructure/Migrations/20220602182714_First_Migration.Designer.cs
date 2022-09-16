﻿// <auto-generated />
using System;
using Agenda.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Agenda.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220602182714_First_Migration")]
    partial class First_Migration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Agenda.Domain.Models.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("tb_contact", (string)null);
                });

            modelBuilder.Entity("Agenda.Domain.Models.Enumerations.InteractionType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("InteractionTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Create Contact"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Update Contact"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Delete Contact"
                        },
                        new
                        {
                            Id = 4,
                            Name = "View Contact"
                        },
                        new
                        {
                            Id = 5,
                            Name = "View Phones"
                        });
                });

            modelBuilder.Entity("Agenda.Domain.Models.Enumerations.PhoneType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("PhoneTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Residencial"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Cellphone"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Commercial"
                        });
                });

            modelBuilder.Entity("Agenda.Domain.Models.Enumerations.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Common"
                        });
                });

            modelBuilder.Entity("Agenda.Domain.Models.Interaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("InteractionTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("message");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InteractionTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("tb_interaction", (string)null);
                });

            modelBuilder.Entity("Agenda.Domain.Models.Phone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ContactId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("DDD")
                        .HasMaxLength(2)
                        .HasColumnType("int")
                        .HasColumnName("ddd");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("description");

                    b.Property<string>("FormattedNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("formattedNumber");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)")
                        .HasColumnName("number");

                    b.Property<int>("PhoneTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ContactId");

                    b.HasIndex("PhoneTypeId");

                    b.ToTable("tb_phone", (string)null);
                });

            modelBuilder.Entity("Agenda.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("password");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserRoleId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("userName");

                    b.HasKey("Id");

                    b.HasIndex("UserRoleId");

                    b.ToTable("tb_user", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2022, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@api.com",
                            Name = "Admin Root Application",
                            Password = "AQAAAAEAAAPoAAAAEIPCxPofH8YJsYljlF4SGhisL9z4jIuhMOc+WTKFZb28XNSRMXUmraNC2WYwd9vYHQ==",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserRoleId = 1,
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("Agenda.Domain.Models.Contact", b =>
                {
                    b.HasOne("Agenda.Domain.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Agenda.Domain.Models.Interaction", b =>
                {
                    b.HasOne("Agenda.Domain.Models.Enumerations.InteractionType", "InteractionType")
                        .WithMany()
                        .HasForeignKey("InteractionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Agenda.Domain.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InteractionType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Agenda.Domain.Models.Phone", b =>
                {
                    b.HasOne("Agenda.Domain.Models.Contact", "Contact")
                        .WithMany("Phones")
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Agenda.Domain.Models.Enumerations.PhoneType", "PhoneType")
                        .WithMany()
                        .HasForeignKey("PhoneTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Contact");

                    b.Navigation("PhoneType");
                });

            modelBuilder.Entity("Agenda.Domain.Models.User", b =>
                {
                    b.HasOne("Agenda.Domain.Models.Enumerations.UserRole", "UserRole")
                        .WithMany()
                        .HasForeignKey("UserRoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("Agenda.Domain.Models.Contact", b =>
                {
                    b.Navigation("Phones");
                });
#pragma warning restore 612, 618
        }
    }
}
