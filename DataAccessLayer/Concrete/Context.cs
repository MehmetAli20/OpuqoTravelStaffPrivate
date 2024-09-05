using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class Context : IdentityDbContext<Staff, Role<int>, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("server=DESKTOP-5JOA5TP;database=TravelStaffDB;integrated security=true;");
            optionsBuilder.UseSqlServer("server=DESKTOP-5JOA5TP;database=TravelStaff_Test5;integrated security=true;");
        }

        public DbSet<Travel> Travels { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Message> Messages { get; set; }
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Self - referencing relationship for Staff
            base.OnModelCreating(modelBuilder);

           modelBuilder.Entity<Staff>()
               .HasOne(s => s.Admin)
               .WithMany()
               .HasForeignKey(s => s.AdminID)
               .OnDelete(DeleteBehavior.NoAction); // Configure delete behavior

           // Relationships for Travel
           modelBuilder.Entity<Travel>()
                .HasOne(t => t.Staff)
                .WithMany(s => s.Travels)
                .HasForeignKey(t => t.Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Travel>()
                .HasOne(t => t.Admin)
                .WithMany()
                .HasForeignKey(t => t.AdminID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Travel>()
                .HasOne(t => t.Status)
                .WithMany(s => s.Travels)
                .HasForeignKey(t => t.StatusID)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<Travel>()
                .HasMany(t => t.Messages)
                .WithOne(m => m.Travel)
                .HasForeignKey(m => m.TravelID)
                .OnDelete(DeleteBehavior.NoAction);

            // modelBuilder.Entity<Staff>()
            //.HasMany(s => s.Travels)
            //.WithOne(t => t.Staff)
            //.HasForeignKey(t => t.StaffID)
            //.OnDelete(DeleteBehavior.NoAction); //staff silinirse travelda staffid 

            // modelBuilder.Entity<Staff>()
            // .HasOne(s => s.Admin)
            // .WithMany(a => a.Staffs)
            // .HasForeignKey(s => s.AdminID)
            // .OnDelete(DeleteBehavior.NoAction);

            // //Check Constraint -Veritabanı seviyesinde kontrol ekleme
            // modelBuilder.Entity<Staff>()
            //     .HasCheckConstraint("CK_Staff_AdminID_IsAdmin",
            //         "([IsAdmin] = 1 AND [AdminID] IS NULL) OR ([IsAdmin] = 0 AND [AdminID] IS NOT NULL)");


            // modelBuilder.Entity<Travel>()
            //     .HasOne(t => t.Admin)
            //     .WithMany()
            //     .HasForeignKey(t => t.AdminID)
            //     .OnDelete(DeleteBehavior.NoAction);

            // modelBuilder.Entity<Travel>()
            //     .HasOne(t => t.Status)
            //     .WithMany(s => s.Travels)
            //     .HasForeignKey(t => t.StatusID)
            //     .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
