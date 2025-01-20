using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SalesDatePredictionBack.Core.Entities;



namespace SalesDatePredictionBack.Infraestructure.Data
{
    public class StoreSampleContext : DbContext
    {
        public StoreSampleContext(DbContextOptions<StoreSampleContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<SaleDatePrediction> SaleDatesPrediction { get; set; }
        public DbSet<OrderToGet> ordersToGet { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>().HasNoKey();
            //modelBuilder.Entity<Order>().HasNoKey();
            modelBuilder.Entity<SaleDatePrediction>().HasNoKey();
            base.OnModelCreating(modelBuilder);

        }
    }
}
