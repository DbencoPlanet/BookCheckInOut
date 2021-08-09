﻿// <auto-generated />
using System;
using BookCheckInOut.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BookCheckInOut.Data.Migrations
{
    [DbContext(typeof(BookCheckInOutDBContext))]
    [Migration("20210806102642_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BookCheckInOut.Core.Books.Book", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("BookCheckInOut.Core.Checkins.Checkin", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("BookId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CheckedinDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("PersonId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("PersonId");

                    b.ToTable("Checkins");
                });

            modelBuilder.Entity("BookCheckInOut.Core.Checkouts.Checkout", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("BookId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CheckoutDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("PersonId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("PersonId");

                    b.ToTable("Checkouts");
                });

            modelBuilder.Entity("BookCheckInOut.Core.Checkouts.CheckoutHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("BookId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CheckedIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CheckedOut")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("PenaltyFee")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long?>("PersonId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("PersonId");

                    b.ToTable("CheckoutHistories");
                });

            modelBuilder.Entity("BookCheckInOut.Core.Persons.Person", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NationalID")
                        .HasColumnType("int");

                    b.Property<decimal>("PenaltyFee")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("BookCheckInOut.Core.Checkins.Checkin", b =>
                {
                    b.HasOne("BookCheckInOut.Core.Books.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId");

                    b.HasOne("BookCheckInOut.Core.Persons.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId");

                    b.Navigation("Book");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("BookCheckInOut.Core.Checkouts.Checkout", b =>
                {
                    b.HasOne("BookCheckInOut.Core.Books.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId");

                    b.HasOne("BookCheckInOut.Core.Persons.Person", "Person")
                        .WithMany("Checkouts")
                        .HasForeignKey("PersonId");

                    b.Navigation("Book");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("BookCheckInOut.Core.Checkouts.CheckoutHistory", b =>
                {
                    b.HasOne("BookCheckInOut.Core.Books.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId");

                    b.HasOne("BookCheckInOut.Core.Persons.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId");

                    b.Navigation("Book");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("BookCheckInOut.Core.Persons.Person", b =>
                {
                    b.Navigation("Checkouts");
                });
#pragma warning restore 612, 618
        }
    }
}