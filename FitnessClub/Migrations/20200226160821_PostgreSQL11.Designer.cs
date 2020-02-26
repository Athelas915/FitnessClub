﻿// <auto-generated />
using System;
using FitnessClub.Data.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FitnessClub.Migrations
{
    [DbContext(typeof(FCContext))]
    [Migration("20200226160821_PostgreSQL11")]
    partial class PostgreSQL11
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("FitnessClub.Data.Models.Adress", b =>
                {
                    b.Property<int>("AdressID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("PersonID")
                        .HasColumnType("integer");

                    b.Property<string>("Region")
                        .HasColumnType("text");

                    b.Property<string>("Street")
                        .HasColumnType("text");

                    b.Property<string>("ZipCode")
                        .HasColumnType("text");

                    b.HasKey("AdressID");

                    b.HasIndex("PersonID");

                    b.ToTable("Adresses");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.CoachRating", b =>
                {
                    b.Property<int>("CoachRatingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CoachID")
                        .HasColumnType("integer");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.HasKey("CoachRatingID");

                    b.HasIndex("CoachID");

                    b.ToTable("CoachRatings");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Holiday", b =>
                {
                    b.Property<int>("HolidayID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Finish")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("Start")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("HolidayID");

                    b.HasIndex("EmployeeID");

                    b.ToTable("Holidays");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Membership", b =>
                {
                    b.Property<int>("MembershipID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("CustomerID")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Finish")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("MembershipNo")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Start")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("MembershipID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Memberships");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Person", b =>
                {
                    b.Property<int>("PersonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer");

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

                    b.ToTable("People");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Person");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Session", b =>
                {
                    b.Property<int>("SessionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CoachID")
                        .HasColumnType("integer");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("Finish")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Room")
                        .HasColumnType("integer");

                    b.Property<int?>("SessionType")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Start")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("SessionID");

                    b.HasIndex("CoachID");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.SessionEnrollment", b =>
                {
                    b.Property<int>("SessionEnrollmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("CustomerID")
                        .HasColumnType("integer");

                    b.Property<int?>("SessionID")
                        .HasColumnType("integer");

                    b.HasKey("SessionEnrollmentID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("SessionID");

                    b.ToTable("SessionEnrollments");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Customer", b =>
                {
                    b.HasBaseType("FitnessClub.Data.Models.Person");

                    b.Property<int>("CustomerID")
                        .HasColumnType("integer");

                    b.Property<int?>("PersonID1")
                        .HasColumnType("integer");

                    b.HasIndex("PersonID1");

                    b.HasDiscriminator().HasValue("Customer");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Employee", b =>
                {
                    b.HasBaseType("FitnessClub.Data.Models.Person");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("integer");

                    b.Property<int?>("PersonID1")
                        .HasColumnName("Employee_PersonID1")
                        .HasColumnType("integer");

                    b.HasIndex("PersonID1");

                    b.HasDiscriminator().HasValue("Employee");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Coach", b =>
                {
                    b.HasBaseType("FitnessClub.Data.Models.Employee");

                    b.Property<int>("CoachID")
                        .HasColumnType("integer");

                    b.HasIndex("EmployeeID");

                    b.HasDiscriminator().HasValue("Coach");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Adress", b =>
                {
                    b.HasOne("FitnessClub.Data.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonID");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.CoachRating", b =>
                {
                    b.HasOne("FitnessClub.Data.Models.Coach", "Coach")
                        .WithMany()
                        .HasForeignKey("CoachID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Holiday", b =>
                {
                    b.HasOne("FitnessClub.Data.Models.Employee", "Employee")
                        .WithMany("Holidays")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Membership", b =>
                {
                    b.HasOne("FitnessClub.Data.Models.Customer", "Customer")
                        .WithMany("Memberships")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Session", b =>
                {
                    b.HasOne("FitnessClub.Data.Models.Coach", "Coach")
                        .WithMany()
                        .HasForeignKey("CoachID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FitnessClub.Data.Models.SessionEnrollment", b =>
                {
                    b.HasOne("FitnessClub.Data.Models.Customer", "Customer")
                        .WithMany("SessionEnrollments")
                        .HasForeignKey("CustomerID");

                    b.HasOne("FitnessClub.Data.Models.Session", "Session")
                        .WithMany("SessionEnrollments")
                        .HasForeignKey("SessionID");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Customer", b =>
                {
                    b.HasOne("FitnessClub.Data.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonID1");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Employee", b =>
                {
                    b.HasOne("FitnessClub.Data.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonID1");
                });

            modelBuilder.Entity("FitnessClub.Data.Models.Coach", b =>
                {
                    b.HasOne("FitnessClub.Data.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
