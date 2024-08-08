using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class Context:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-5JOA5TP;database=TravelStaffDB;integrated security=true;");
        }

        public DbSet<Travel> Travels { get; set; }
        public DbSet<Staff> Staffs { get; set; }
       // public DbSet<Admin> Admins { get; set; }
        public DbSet<Status> Statuses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Admin>()
           //.HasMany(a => a.Staffs)
           //.WithOne(s => s.Admin)
           //.HasForeignKey(s => s.AdminID);

            //modelBuilder.Entity<Admin>()
            //.HasMany(a => a.Staffs)
            //.WithOne(s => s.Admin)
            //.HasForeignKey(s => s.AdminID)
            //.OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Staff>()
           .HasMany(s => s.Travels)
           .WithOne(t => t.Staff)
           .HasForeignKey(t => t.StaffID)
           .OnDelete(DeleteBehavior.NoAction);
         
        }
    }
}
