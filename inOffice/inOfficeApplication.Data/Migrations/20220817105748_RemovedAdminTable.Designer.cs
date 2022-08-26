﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using inOfficeApplication.Data;

#nullable disable

namespace inOfficeApplication.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220817105748_RemovedAdminTable")]
    partial class RemovedAdminTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("inOfficeApplication.Data.Models.Categories", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DeskId")
                        .HasColumnType("int");

                    b.Property<bool>("DoubleMonitor")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("NearWindow")
                        .HasColumnType("bit");

                    b.Property<bool>("SingleMonitor")
                        .HasColumnType("bit");

                    b.Property<bool>("Unavailable")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("inOfficeApplication.Data.Models.ConferenceRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int?>("IndexForOffice")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("OfficeId")
                        .HasColumnType("int");

                    b.Property<int?>("ReservationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OfficeId");

                    b.HasIndex("ReservationId")
                        .IsUnique()
                        .HasFilter("[ReservationId] IS NOT NULL");

                    b.ToTable("ConferenceRooms");
                });

            modelBuilder.Entity("inOfficeApplication.Data.Models.ConferenceRoomMode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ConferenceRoomId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("ModeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ConferenceRoomId");

                    b.HasIndex("ModeId");

                    b.ToTable("ConferenceRoomModes");
                });

            modelBuilder.Entity("inOfficeApplication.Data.Models.Desk", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CategorieId")
                        .HasColumnType("int");

                    b.Property<string>("Categories")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IndexForOffice")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("OfficeId")
                        .HasColumnType("int");

                    b.Property<int?>("ReservationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OfficeId");

                    b.HasIndex("ReservationId")
                        .IsUnique()
                        .HasFilter("[ReservationId] IS NOT NULL");

                    b.ToTable("Desks");
                });

            modelBuilder.Entity("inOfficeApplication.Data.Models.DeskCategories", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("DeskId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("DeskId");

                    b.ToTable("DeskCategories");
                });

            modelBuilder.Entity("inOfficeApplication.Data.Models.DeskMode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DeskId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("ModeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeskId");

                    b.HasIndex("ModeId");

                    b.ToTable("DeskModes");
                });

            modelBuilder.Entity("inOfficeApplication.Data.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("JobTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "user@inoffice.com",
                            FirstName = "Nekoj Employee",
                            IsDeleted = false,
                            LastName = "Prezime Employee",
                            Password = "$2a$11$Nhp5LeLiLWYm5xXKQW1RuOA7xVL90vxwmSLHLhIzRdP8FM9C20TH2"
                        });
                });

            modelBuilder.Entity("inOfficeApplication.Data.Models.Mode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OfficeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OfficeId");

                    b.ToTable("Modes");
                });

            modelBuilder.Entity("inOfficeApplication.Data.Models.Office", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OfficeImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Offices");
                });

            modelBuilder.Entity("inOfficeApplication.Data.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ConferenceRoomId")
                        .HasColumnType("int");

                    b.Property<int?>("DeskId")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int?>("ReviewId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("inOfficeApplication.Data.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.Property<string>("ReviewOutput")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reviews")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("ReservationId")
                        .IsUnique();

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("inOfficeApplication.Data.Models.ConferenceRoom", b =>
                {
                    b.HasOne("inOfficeApplication.Data.Models.Office", "Office")
                        .WithMany("ConferenceRooms")
                        .HasForeignKey("OfficeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("inOfficeApplication.Data.Models.Reservation", "Reservation")
                        .WithOne("ConferenceRoom")
                        .HasForeignKey("inOfficeApplication.Data.Models.ConferenceRoom", "ReservationId");

                    b.Navigation("Office");

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("inOfficeApplication.Data.Models.ConferenceRoomMode", b =>
                {
                    b.HasOne("inOfficeApplication.Data.Models.ConferenceRoom", "ConferenceRoom")
                        .WithMany("ConferenceRoomModes")
                        .HasForeignKey("ConferenceRoomId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("inOfficeApplication.Data.Models.Mode", "Mode")
                        .WithMany("ConferenceRoomModes")
                        .HasForeignKey("ModeId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("ConferenceRoom");

                    b.Navigation("Mode");
                });

            modelBuilder.Entity("inOfficeApplication.Data.Models.Desk", b =>
                {
                    b.HasOne("inOfficeApplication.Data.Models.Office", "Office")
                        .WithMany("Desks")
                        .HasForeignKey("OfficeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("inOfficeApplication.Data.Models.Reservation", "Reservation")
                        .WithOne("Desk")
                        .HasForeignKey("inOfficeApplication.Data.Models.Desk", "ReservationId");

                    b.Navigation("Office");

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("inOfficeApplication.Data.Models.DeskCategories", b =>
                {
                    b.HasOne("inOfficeApplication.Data.Models.Desk", "Desk")
                        .WithMany("DeskCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("inOfficeApplication.Data.Models.Categories", "Categorie")
                        .WithMany("DeskCategories")
                        .HasForeignKey("DeskId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Categorie");

                    b.Navigation("Desk");
                });

            modelBuilder.Entity("inOfficeApplication.Data.Models.DeskMode", b =>
                {
                    b.HasOne("inOfficeApplication.Data.Models.Desk", "Desk")
                        .WithMany("DeskModes")
                        .HasForeignKey("DeskId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("inOfficeApplication.Data.Models.Mode", "Mode")
                        .WithMany("DeskModes")
                        .HasForeignKey("ModeId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Desk");

                    b.Navigation("Mode");
                });

            modelBuilder.Entity("inOfficeApplication.Data.Models.Mode", b =>
                {
                    b.HasOne("inOfficeApplication.Data.Models.Office", "Office")
                        .WithMany("Modes")
                        .HasForeignKey("OfficeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Office");
                });

            modelBuilder.Entity("inOfficeApplication.Data.Models.Reservation", b =>
                {
                    b.HasOne("inOfficeApplication.Data.Models.Employee", "Employee")
                        .WithMany("Reservations")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("inOfficeApplication.Data.Models.Review", b =>
                {
                    b.HasOne("inOfficeApplication.Data.Models.Reservation", "Reservation")
                        .WithOne("Review")
                        .HasForeignKey("inOfficeApplication.Data.Models.Review", "ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("inOfficeApplication.Data.Models.Categories", b =>
                {
                    b.Navigation("DeskCategories");
                });

            modelBuilder.Entity("inOfficeApplication.Data.Models.ConferenceRoom", b =>
                {
                    b.Navigation("ConferenceRoomModes");
                });

            modelBuilder.Entity("inOfficeApplication.Data.Models.Desk", b =>
                {
                    b.Navigation("DeskCategories");

                    b.Navigation("DeskModes");
                });

            modelBuilder.Entity("inOfficeApplication.Data.Models.Employee", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("inOfficeApplication.Data.Models.Mode", b =>
                {
                    b.Navigation("ConferenceRoomModes");

                    b.Navigation("DeskModes");
                });

            modelBuilder.Entity("inOfficeApplication.Data.Models.Office", b =>
                {
                    b.Navigation("ConferenceRooms");

                    b.Navigation("Desks");

                    b.Navigation("Modes");
                });

            modelBuilder.Entity("inOfficeApplication.Data.Models.Reservation", b =>
                {
                    b.Navigation("ConferenceRoom");

                    b.Navigation("Desk");

                    b.Navigation("Review");
                });
#pragma warning restore 612, 618
        }
    }
}