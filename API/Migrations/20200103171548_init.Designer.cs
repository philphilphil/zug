﻿// <auto-generated />
using API;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ControllerApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200103171548_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0");

            modelBuilder.Entity("API.Switch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CurrentState")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("RPI_Pin1")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RPI_Pin2")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Switches");
                });
#pragma warning restore 612, 618
        }
    }
}
