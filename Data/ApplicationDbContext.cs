using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentalRides.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalRides.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        // Define Model Classes
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Segment> Segments { get; set; }
        public DbSet<BookingList> BookingLists { get; set; }
        public DbSet<BookingDetail> BookingDetails { get; set; }
        public DbSet<Car> Cars { get; set; }

        //Override the model creating method
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Define Relationships
                //Car and Segment
            builder.Entity<Car>()
                .HasOne(c => c.Segment)
                .WithMany(s => s.Cars)
                .HasForeignKey(c => c.SegmentId)
                .HasConstraintName("FK_Cars_SegmentID");

                //BookingDetail and Car
            builder.Entity<BookingDetail>()
                .HasOne(c => c.Car)
                .WithMany(s => s.BookingDetails)
                .HasForeignKey(c => c.CarId)
                .HasConstraintName("FK_BookingDetails_CarID");

                //Cart and Car
            builder.Entity<Cart>()
                .HasOne(c => c.Car)
                .WithMany(s => s.Carts)
                .HasForeignKey(c => c.CarId)
                .HasConstraintName("FK_Carts_CarID");
                
                //BookingDetail and BookingList
            builder.Entity<BookingDetail>()
                .HasOne(c => c.BookingList)
                .WithMany(s => s.BookingDetails)
                .HasForeignKey(c => c.BookingListId)
                .HasConstraintName("FK_BookingDetails_BookingListID");
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
