using Microsoft.EntityFrameworkCore;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DatabaseContext
{
    public class InsuranceDbContext : DbContext
    {
        public InsuranceDbContext(DbContextOptions<InsuranceDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FraudClaim>(d =>
            {
                d.HasKey("InsurerId");
                d.ToView("View_Claims");
            });
        }


    public DbSet<FraudClaim> Claims { get; set; }

    }
}
