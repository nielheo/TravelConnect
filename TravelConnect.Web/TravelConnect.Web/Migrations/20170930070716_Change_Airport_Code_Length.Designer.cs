﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using TravelConnect.Models;

namespace TravelConnect.Web.Migrations
{
    [DbContext(typeof(TCContext))]
    [Migration("20170930070716_Change_Airport_Code_Length")]
    partial class Change_Airport_Code_Length
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TravelConnect.Models.Airline", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(2);

                    b.Property<DateTime>("CreatedTime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("UpdatedTime");

                    b.HasKey("Id");

                    b.ToTable("Airlines");
                });

            modelBuilder.Entity("TravelConnect.Models.Airport", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(3);

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasMaxLength(2);

                    b.Property<DateTime>("CreatedTime");

                    b.Property<float>("Latitude");

                    b.Property<float>("Longitude");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("UpdatedTime");

                    b.HasKey("Id");

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("TravelConnect.Models.SabreCredential", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccessToken")
                        .HasMaxLength(300);

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("ClientSecret")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<DateTime?>("ExpiryTime");

                    b.Property<bool>("IsActive");

                    b.HasKey("Id");

                    b.ToTable("SabreCredentials");
                });

            modelBuilder.Entity("TravelConnect.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedTime");

                    b.Property<bool>("IsActive");

                    b.Property<DateTime>("LastLoginTime");

                    b.Property<string>("LoginName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<decimal>("Markup");

                    b.Property<int?>("ParentUserId");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int>("UpdatedBy");

                    b.Property<DateTime>("UpdatedTime");

                    b.Property<int>("UserLevel");

                    b.HasKey("Id");

                    b.HasIndex("ParentUserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TravelConnect.Models.User", b =>
                {
                    b.HasOne("TravelConnect.Models.User", "ParentUser")
                        .WithMany("ChildUsers")
                        .HasForeignKey("ParentUserId");
                });
#pragma warning restore 612, 618
        }
    }
}
