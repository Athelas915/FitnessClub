﻿// <auto-generated />
using System;
using FitnessClub.Data.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FitnessClub.Migrations
{
    [DbContext(typeof(FCContext))]
    partial class FCContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("FitnessClub.Data.Models.Address", b =>
                {
                    b.Property<int>("AddressID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<int>("CreatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<int?>("PersonID")
                        .HasColumnType("integer");

                    b.Property<string>("Region")
                        .HasColumnType("text");

                    b.Property<string>("Street")
                        .HasColumnType("text");

                    b.Property<string>("ZipCode")
                        .HasColumnType("text");

                    b.HasKey("AddressID");

                    b.HasIndex("PersonID");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.CoachRating", b =>
                {
                    b.Property<int>("CoachRatingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("CoachPersonID")
                        .HasColumnType("integer");

                    b.Property<int>("CreatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<int>("PersonID")
                        .HasColumnType("integer");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.HasKey("CoachRatingID");

                    b.HasIndex("CoachPersonID");

                    b.ToTable("CoachRatings");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Holiday", b =>
                {
                    b.Property<int>("HolidayID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CreatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<int?>("EmployeePersonID")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Finish")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("PersonID")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Start")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("HolidayID");

                    b.HasIndex("EmployeePersonID");

                    b.ToTable("Holidays");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Identity.AspNetRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Identity.AspNetUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Exception")
                        .HasColumnName("exception")
                        .HasColumnType("text");

                    b.Property<int>("Level")
                        .HasColumnName("level")
                        .HasColumnType("int4");

                    b.Property<string>("Log_event")
                        .HasColumnName("log_event")
                        .HasColumnType("jsonb");

                    b.Property<string>("Message")
                        .HasColumnName("message")
                        .HasColumnType("text");

                    b.Property<string>("Message_template")
                        .HasColumnName("message_template")
                        .HasColumnType("text");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnName("timestamp")
                        .HasColumnType("timestamp");

                    b.HasKey("Id");

                    b.ToTable("logs");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Membership", b =>
                {
                    b.Property<int>("MembershipID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CreatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<int?>("CustomerPersonID")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Finish")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("MembershipNo")
                        .HasColumnType("integer");

                    b.Property<int>("PersonID")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Start")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("MembershipID");

                    b.HasIndex("CustomerPersonID");

                    b.ToTable("Memberships");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Person", b =>
                {
                    b.Property<int>("PersonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("AspNetUserId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("CreatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<int?>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.HasKey("PersonID");

                    b.HasIndex("AspNetUserId")
                        .IsUnique();

                    b.ToTable("People");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Person");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Session", b =>
                {
                    b.Property<int>("SessionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("CoachPersonID")
                        .HasColumnType("integer");

                    b.Property<int>("CreatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("Finish")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("PersonID")
                        .HasColumnType("integer");

                    b.Property<int>("Room")
                        .HasColumnType("integer");

                    b.Property<int?>("SessionType")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Start")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("SessionID");

                    b.HasIndex("CoachPersonID");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.SessionEnrollment", b =>
                {
                    b.Property<int?>("PersonID")
                        .HasColumnType("integer");

                    b.Property<int?>("SessionID")
                        .HasColumnType("integer");

                    b.Property<int>("CreatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<int?>("CustomerPersonID")
                        .HasColumnType("integer");

                    b.HasKey("PersonID", "SessionID");

                    b.HasIndex("CustomerPersonID");

                    b.HasIndex("SessionID");

                    b.ToTable("SessionEnrollments");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityRoleClaim<int>");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUserClaim<int>");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUserLogin<int>");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUserRole<int>");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUserToken<int>");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Customer", b =>
                {
                    b.HasBaseType("FitnessClub.Data.Models.Person");

                    b.HasDiscriminator().HasValue("Customer");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Employee", b =>
                {
                    b.HasBaseType("FitnessClub.Data.Models.Person");

                    b.HasDiscriminator().HasValue("Employee");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Identity.AspNetRoleClaim", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>");

                    b.Property<int?>("AspNetRoleId")
                        .HasColumnType("integer");

                    b.HasIndex("AspNetRoleId");

                    b.HasDiscriminator().HasValue("AspNetRoleClaim");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Identity.AspNetUserClaim", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>");

                    b.Property<int?>("AspNetUserId")
                        .HasColumnType("integer");

                    b.HasIndex("AspNetUserId");

                    b.HasDiscriminator().HasValue("AspNetUserClaim");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Identity.AspNetUserLogin", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>");

                    b.Property<int?>("AspNetUserId")
                        .HasColumnType("integer");

                    b.HasIndex("AspNetUserId");

                    b.HasDiscriminator().HasValue("AspNetUserLogin");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Identity.AspNetUserRole", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUserRole<int>");

                    b.Property<int?>("AspNetRoleId")
                        .HasColumnType("integer");

                    b.Property<int?>("AspNetUserId")
                        .HasColumnType("integer");

                    b.HasIndex("AspNetRoleId");

                    b.HasIndex("AspNetUserId");

                    b.HasDiscriminator().HasValue("AspNetUserRole");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Identity.AspNetUserToken", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUserToken<int>");

                    b.Property<int?>("AspNetUserId")
                        .HasColumnType("integer");

                    b.HasIndex("AspNetUserId");

                    b.HasDiscriminator().HasValue("AspNetUserToken");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Coach", b =>
                {
                    b.HasBaseType("FitnessClub.Data.Models.Employee");

                    b.HasDiscriminator().HasValue("Coach");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Address", b =>
                {
                    b.HasOne("FitnessClub.Data.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonID");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.CoachRating", b =>
                {
                    b.HasOne("FitnessClub.Data.Models.Coach", "Coach")
                        .WithMany("CoachRatings")
                        .HasForeignKey("CoachPersonID");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Holiday", b =>
                {
                    b.HasOne("FitnessClub.Data.Models.Employee", "Employee")
                        .WithMany("Holidays")
                        .HasForeignKey("EmployeePersonID");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Membership", b =>
                {
                    b.HasOne("FitnessClub.Data.Models.Customer", "Customer")
                        .WithMany("Memberships")
                        .HasForeignKey("CustomerPersonID");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Person", b =>
                {
                    b.HasOne("FitnessClub.Data.Models.Identity.AspNetUser", "AspNetUser")
                        .WithOne("Person")
                        .HasForeignKey("FitnessClub.Data.Models.Person", "AspNetUserId");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Session", b =>
                {
                    b.HasOne("FitnessClub.Data.Models.Coach", "Coach")
                        .WithMany()
                        .HasForeignKey("CoachPersonID");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.SessionEnrollment", b =>
                {
                    b.HasOne("FitnessClub.Data.Models.Customer", "Customer")
                        .WithMany("SessionEnrollments")
                        .HasForeignKey("CustomerPersonID");

                    b.HasOne("FitnessClub.Data.Models.Session", "Session")
                        .WithMany("SessionEnrollments")
                        .HasForeignKey("SessionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("FitnessClub.Data.Models.Identity.AspNetRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("FitnessClub.Data.Models.Identity.AspNetUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("FitnessClub.Data.Models.Identity.AspNetUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("FitnessClub.Data.Models.Identity.AspNetRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FitnessClub.Data.Models.Identity.AspNetUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("FitnessClub.Data.Models.Identity.AspNetUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Identity.AspNetRoleClaim", b =>
                {
                    b.HasOne("FitnessClub.Data.Models.Identity.AspNetRole", "AspNetRole")
                        .WithMany("AspNetRoleClaims")
                        .HasForeignKey("AspNetRoleId");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Identity.AspNetUserClaim", b =>
                {
                    b.HasOne("FitnessClub.Data.Models.Identity.AspNetUser", "AspNetUser")
                        .WithMany("AspNetUserClaims")
                        .HasForeignKey("AspNetUserId");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Identity.AspNetUserLogin", b =>
                {
                    b.HasOne("FitnessClub.Data.Models.Identity.AspNetUser", "AspNetUser")
                        .WithMany("AspNetUserLogins")
                        .HasForeignKey("AspNetUserId");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Identity.AspNetUserRole", b =>
                {
                    b.HasOne("FitnessClub.Data.Models.Identity.AspNetRole", "AspNetRole")
                        .WithMany("AspNetUserRoles")
                        .HasForeignKey("AspNetRoleId");

                    b.HasOne("FitnessClub.Data.Models.Identity.AspNetUser", "AspNetUser")
                        .WithMany("AspNetUserRoles")
                        .HasForeignKey("AspNetUserId");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Identity.AspNetUserToken", b =>
                {
                    b.HasOne("FitnessClub.Data.Models.Identity.AspNetUser", "AspNetUser")
                        .WithMany("AspNetUserTokens")
                        .HasForeignKey("AspNetUserId");
                });
#pragma warning restore 612, 618
        }
    }
}
