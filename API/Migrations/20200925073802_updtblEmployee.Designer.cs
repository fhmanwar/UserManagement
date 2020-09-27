﻿// <auto-generated />
using System;
using API.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20200925073802_updtblEmployee")]
    partial class updtblEmployee
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("API.Models.Division", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreateDate");

                    b.Property<DateTimeOffset>("DeleteDate");

                    b.Property<string>("Name");

                    b.Property<DateTimeOffset>("UpdateDate");

                    b.Property<bool>("isDelete");

                    b.HasKey("Id");

                    b.ToTable("TB_M_Division");
                });

            modelBuilder.Entity("API.Models.Employee", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("Address");

                    b.Property<string>("AssignmentSite");

                    b.Property<string>("City");

                    b.Property<DateTimeOffset>("CreateDate");

                    b.Property<DateTimeOffset>("DeleteDate");

                    b.Property<string>("DivisionId");

                    b.Property<string>("NIK");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<string>("Province");

                    b.Property<string>("SubDistrict");

                    b.Property<DateTimeOffset>("UpdateDate");

                    b.Property<string>("Village");

                    b.Property<string>("ZipCode");

                    b.Property<bool>("isDelete");

                    b.HasKey("UserId");

                    b.HasIndex("DivisionId");

                    b.ToTable("TB_M_Employee");
                });

            modelBuilder.Entity("API.Models.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("CreateDate");

                    b.Property<string>("Email");

                    b.Property<string>("Response");

                    b.HasKey("Id");

                    b.ToTable("TB_T_Log");
                });

            modelBuilder.Entity("API.Models.Role", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<DateTimeOffset>("CreateDate");

                    b.Property<DateTimeOffset>("DeleteDate");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedName");

                    b.Property<DateTimeOffset>("UpdateDate");

                    b.Property<bool>("isDelete");

                    b.HasKey("Id");

                    b.ToTable("TB_M_Role");
                });

            modelBuilder.Entity("API.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Password");

                    b.Property<string>("Token");

                    b.Property<string>("VerifyCode");

                    b.HasKey("Id");

                    b.ToTable("TB_M_User");
                });

            modelBuilder.Entity("API.Models.UserRole", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("TB_M_UserRole");
                });

            modelBuilder.Entity("API.Models.Employee", b =>
                {
                    b.HasOne("API.Models.Division", "Division")
                        .WithMany()
                        .HasForeignKey("DivisionId");

                    b.HasOne("API.Models.User", "User")
                        .WithOne("Employee")
                        .HasForeignKey("API.Models.Employee", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("API.Models.UserRole", b =>
                {
                    b.HasOne("API.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("API.Models.User", "User")
                        .WithMany("userRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
