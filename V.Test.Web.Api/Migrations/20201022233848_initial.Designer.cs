﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using V.Test.Web.Api.Repository;

namespace V.Test.Web.Api.Migrations
{
    [DbContext(typeof(VTestsContext))]
    [Migration("20201022233848_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("V.Test.Web.Api.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.Property<string>("AddressLine2")
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.Property<string>("AddressLine3")
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.Property<string>("AddressLine4")
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<bool?>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Postcode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.Property<string>("Town")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("V.Test.Web.Api.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.Property<bool?>("IsDeleted");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<int>("OrganisationId");

                    b.HasKey("Id");

                    b.HasIndex("OrganisationId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("V.Test.Web.Api.Entities.Organisation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AddressId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<bool?>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("OrganisationName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.Property<string>("OrganisationNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Organisation");
                });

            modelBuilder.Entity("V.Test.Web.Api.Entities.Employee", b =>
                {
                    b.HasOne("V.Test.Web.Api.Entities.Organisation", "Organisation")
                        .WithMany("Employee")
                        .HasForeignKey("OrganisationId")
                        .HasConstraintName("FK_Employee_Organisation");
                });

            modelBuilder.Entity("V.Test.Web.Api.Entities.Organisation", b =>
                {
                    b.HasOne("V.Test.Web.Api.Entities.Address", "Address")
                        .WithMany("Organisation")
                        .HasForeignKey("AddressId")
                        .HasConstraintName("FK_Organisation_Address");
                });
#pragma warning restore 612, 618
        }
    }
}
