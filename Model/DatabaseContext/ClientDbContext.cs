using Microsoft.EntityFrameworkCore;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DatabaseContext
{
    public class ClientDbContext : DbContext
    {
        public ClientDbContext(DbContextOptions<ClientDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<AccountType> AccountTypes { get; set; }

        public DbSet<VehicleType> VehicleTypes { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<VehicleBooking> VehicleBookings { get; set; }

        public DbSet<Equipment> Equipments { get; set; }

        public DbSet<EquipmentCategory> EquipmentCategories { get; set; }

        public DbSet<EquipmentBooking> EquipmentBookings { get; set; }

        public DbSet<Inquiry> Inquiries { get; set; }
    }
}
