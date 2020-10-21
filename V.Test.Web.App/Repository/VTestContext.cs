using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using V.Test.Web.App.Entities;

namespace V.Test.Web.App.Repository
{
    public   class VTestsContext : DbContext
    {
        public VTestsContext()
        {
        }

        public VTestsContext(DbContextOptions<VTestsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Organisation> Organisation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=AVO\\SQLEXPRESS2012;Initial Catalog=VTest;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.AddressLine1)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.AddressLine2)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.AddressLine3)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.AddressLine4)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Postcode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Town)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.Organisation)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.OrganisationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Organisation");
            });

            modelBuilder.Entity<Organisation>(entity =>
            {
                entity.Property(e => e.OrganisationName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.OrganisationNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Organisation)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Organisation_Address");
            });
        }
    }
}
