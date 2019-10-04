using System;
using HotelSpectral.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelSpectral.Data
{
    public class HotelSpectralContext : DbContext
    {
        public HotelSpectralContext (DbContextOptions<HotelSpectralContext> options) : base(options)
        {
            new DbContextOptionsBuilder().EnableSensitiveDataLogging(true);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Guests> Guests { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<AuditTrail> AuditTrails { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
