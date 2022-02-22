﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using inOfficeApplication.Data;

#nullable disable

namespace inOfficeApplication.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220221154650_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("inOfficeApplication.Data.Models.ConferenceRoomMode", b =>
                {
                    b.Property<int>("ConferenceRoomId")
                        .HasColumnType("integer");

                    b.Property<int>("ModeId")
                        .HasColumnType("integer");

                    b.HasKey("ConferenceRoomId", "ModeId");

                    b.HasIndex("ModeId");

                    b.ToTable("ConferenceRoomModes");
                });

            modelBuilder.Entity("inOfficeApplication.Data.Models.DeskMode", b =>
                {
                    b.Property<int>("DeskId")
                        .HasColumnType("integer");

                    b.Property<int>("ModeId")
                        .HasColumnType("integer");

                    b.HasKey("DeskId", "ModeId");

                    b.HasIndex("ModeId");

                    b.ToTable("DeskModes");
                });

            modelBuilder.Entity("inOfficeApplication.Models.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)");

                    b.HasKey("Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("inOfficeApplication.Models.ConferenceRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<int>("OfficeId")
                        .HasColumnType("integer");

                    b.Property<int>("ReservationId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OfficeId");

                    b.HasIndex("ReservationId")
                        .IsUnique();

                    b.ToTable("ConferenceRooms");
                });

            modelBuilder.Entity("inOfficeApplication.Models.Desk", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Categories")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OfficeId")
                        .HasColumnType("integer");

                    b.Property<int>("ReservationId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OfficeId");

                    b.HasIndex("ReservationId")
                        .IsUnique();

                    b.ToTable("Desks");
                });

            modelBuilder.Entity("inOfficeApplication.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)");

                    b.Property<string>("JobTitle")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("inOfficeApplication.Models.Mode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("OfficeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OfficeId");

                    b.ToTable("Modes");
                });

            modelBuilder.Entity("inOfficeApplication.Models.Office", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(55)
                        .HasColumnType("character varying(55)");

                    b.Property<string>("OfficeImage")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Offices");
                });

            modelBuilder.Entity("inOfficeApplication.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("inOfficeApplication.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ReservationId")
                        .HasColumnType("integer");

                    b.Property<string>("ReviewOutput")
                        .HasColumnType("text");

                    b.Property<string>("Reviews")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.HasIndex("ReservationId")
                        .IsUnique();

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("inOfficeApplication.Data.Models.ConferenceRoomMode", b =>
                {
                    b.HasOne("inOfficeApplication.Models.ConferenceRoom", "ConferenceRoom")
                        .WithMany("ConferenceRoomModes")
                        .HasForeignKey("ConferenceRoomId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("inOfficeApplication.Models.Mode", "Mode")
                        .WithMany("ConferenceRoomModes")
                        .HasForeignKey("ModeId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("ConferenceRoom");

                    b.Navigation("Mode");
                });

            modelBuilder.Entity("inOfficeApplication.Data.Models.DeskMode", b =>
                {
                    b.HasOne("inOfficeApplication.Models.Desk", "Desk")
                        .WithMany("DeskModes")
                        .HasForeignKey("DeskId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("inOfficeApplication.Models.Mode", "Mode")
                        .WithMany("DeskModes")
                        .HasForeignKey("ModeId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Desk");

                    b.Navigation("Mode");
                });

            modelBuilder.Entity("inOfficeApplication.Models.ConferenceRoom", b =>
                {
                    b.HasOne("inOfficeApplication.Models.Office", "Office")
                        .WithMany("ConferenceRooms")
                        .HasForeignKey("OfficeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("inOfficeApplication.Models.Reservation", "Reservation")
                        .WithOne("ConferenceRoom")
                        .HasForeignKey("inOfficeApplication.Models.ConferenceRoom", "ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Office");

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("inOfficeApplication.Models.Desk", b =>
                {
                    b.HasOne("inOfficeApplication.Models.Office", "Office")
                        .WithMany("Desks")
                        .HasForeignKey("OfficeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("inOfficeApplication.Models.Reservation", "Reservation")
                        .WithOne("Desk")
                        .HasForeignKey("inOfficeApplication.Models.Desk", "ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Office");

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("inOfficeApplication.Models.Mode", b =>
                {
                    b.HasOne("inOfficeApplication.Models.Office", "Office")
                        .WithMany("Modes")
                        .HasForeignKey("OfficeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Office");
                });

            modelBuilder.Entity("inOfficeApplication.Models.Reservation", b =>
                {
                    b.HasOne("inOfficeApplication.Models.Employee", "Employee")
                        .WithMany("Reservations")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("inOfficeApplication.Models.Review", b =>
                {
                    b.HasOne("inOfficeApplication.Models.Reservation", "Reservation")
                        .WithOne("Review")
                        .HasForeignKey("inOfficeApplication.Models.Review", "ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("inOfficeApplication.Models.ConferenceRoom", b =>
                {
                    b.Navigation("ConferenceRoomModes");
                });

            modelBuilder.Entity("inOfficeApplication.Models.Desk", b =>
                {
                    b.Navigation("DeskModes");
                });

            modelBuilder.Entity("inOfficeApplication.Models.Employee", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("inOfficeApplication.Models.Mode", b =>
                {
                    b.Navigation("ConferenceRoomModes");

                    b.Navigation("DeskModes");
                });

            modelBuilder.Entity("inOfficeApplication.Models.Office", b =>
                {
                    b.Navigation("ConferenceRooms");

                    b.Navigation("Desks");

                    b.Navigation("Modes");
                });

            modelBuilder.Entity("inOfficeApplication.Models.Reservation", b =>
                {
                    b.Navigation("ConferenceRoom");

                    b.Navigation("Desk");

                    b.Navigation("Review");
                });
#pragma warning restore 612, 618
        }
    }
}
