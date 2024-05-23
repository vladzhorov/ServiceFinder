﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ServiceFinder.DAL;

#nullable disable

namespace ServiceFinder.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240522134154_UserProfileManagementService")]
    partial class UserProfileManagementService
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ServiceFinder.DAL.Entites.ReviewEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("UserProfileEntityId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.HasIndex("UserProfileEntityId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("ServiceFinder.DAL.Entites.ServiceCategoryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("ServiceCategories");
                });

            modelBuilder.Entity("ServiceFinder.DAL.Entites.ServiceEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("interval");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.Property<Guid>("ServiceCategoryID")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserProfileID")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ServiceCategoryID");

                    b.HasIndex("UserProfileID");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("ServiceFinder.DAL.Entites.UserProfileEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhotoURL")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("UserProfile");
                });

            modelBuilder.Entity("ServiceFinder.DAL.Entites.ReviewEntity", b =>
                {
                    b.HasOne("ServiceFinder.DAL.Entites.ServiceEntity", "Service")
                        .WithMany("Reviews")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceFinder.DAL.Entites.UserProfileEntity", null)
                        .WithMany("Reviews")
                        .HasForeignKey("UserProfileEntityId");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("ServiceFinder.DAL.Entites.ServiceEntity", b =>
                {
                    b.HasOne("ServiceFinder.DAL.Entites.ServiceCategoryEntity", "Category")
                        .WithMany("Services")
                        .HasForeignKey("ServiceCategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceFinder.DAL.Entites.UserProfileEntity", "UserProfile")
                        .WithMany("Services")
                        .HasForeignKey("UserProfileID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("ServiceFinder.DAL.Entites.ServiceCategoryEntity", b =>
                {
                    b.Navigation("Services");
                });

            modelBuilder.Entity("ServiceFinder.DAL.Entites.ServiceEntity", b =>
                {
                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("ServiceFinder.DAL.Entites.UserProfileEntity", b =>
                {
                    b.Navigation("Reviews");

                    b.Navigation("Services");
                });
#pragma warning restore 612, 618
        }
    }
}
