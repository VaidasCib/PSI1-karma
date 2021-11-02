﻿// <auto-generated />
using System;
using Karma.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Karma.Migrations
{
    [DbContext(typeof(KarmaContext))]
    [Migration("20211102100534_EventImages")]
    partial class EventImages
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("CharityEventVolunteer", b =>
                {
                    b.Property<Guid>("EventsId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("VolunteersId")
                        .HasColumnType("TEXT");

                    b.HasKey("EventsId", "VolunteersId");

                    b.HasIndex("VolunteersId");

                    b.ToTable("CharityEventVolunteer");
                });

            modelBuilder.Entity("Karma.Models.CharityEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("ManagerId")
                        .HasColumnType("TEXT");

                    b.Property<int>("MaxVolunteers")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhotoPath")
                        .HasColumnType("TEXT");

                    b.Property<int>("State")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Karma.Models.EventImages", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EventId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("EventImages");
                });

            modelBuilder.Entity("Karma.Models.SpecialEquipment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("SpecialEquipment");
                });

            modelBuilder.Entity("Karma.Models.Volunteer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Volunteers");
                });

            modelBuilder.Entity("CharityEventVolunteer", b =>
                {
                    b.HasOne("Karma.Models.CharityEvent", null)
                        .WithMany()
                        .HasForeignKey("EventsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Karma.Models.Volunteer", null)
                        .WithMany()
                        .HasForeignKey("VolunteersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Karma.Models.SpecialEquipment", b =>
                {
                    b.HasOne("Karma.Models.Volunteer", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");

                    b.Navigation("Owner");
                });
#pragma warning restore 612, 618
        }
    }
}
