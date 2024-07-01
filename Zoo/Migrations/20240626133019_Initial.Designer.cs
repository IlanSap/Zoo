﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZooProject.Data;

#nullable disable

namespace ZooProject.Migrations
{
    [DbContext(typeof(ZooContext))]
    [Migration("20240626133019_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ZooProject.Animals.AnimalTypes.Animal", b =>
                {
                    b.Property<Guid>("AnimalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AnimalBackgroundColor")
                        .HasColumnType("int");

                    b.Property<int>("AnimalForegroundColor")
                        .HasColumnType("int");

                    b.Property<int>("AnimalType")
                        .HasColumnType("int");

                    b.Property<int>("StepSize")
                        .HasColumnType("int");

                    b.Property<Guid?>("ZooId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AnimalId");

                    b.HasIndex("ZooId");

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("ZooProject.Zoo.Zoo", b =>
                {
                    b.Property<Guid>("ZooId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ZooId");

                    b.ToTable("Zoos");
                });

            modelBuilder.Entity("ZooProject.Animals.AnimalTypes.Animal", b =>
                {
                    b.HasOne("ZooProject.Zoo.Zoo", null)
                        .WithMany("_animals")
                        .HasForeignKey("ZooId");
                });

            modelBuilder.Entity("ZooProject.Zoo.Zoo", b =>
                {
                    b.Navigation("_animals");
                });
#pragma warning restore 612, 618
        }
    }
}
