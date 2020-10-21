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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>().ToTable("Address");
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Organisation>().ToTable("Organisation");
        }

    }
}
