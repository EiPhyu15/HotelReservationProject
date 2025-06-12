using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HotelReservationSystemProject.Models;

namespace HotelReservationSystemProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<HotelReservationSystemProject.Models.Guest> Guest { get; set; } = default!;
        public DbSet<HotelReservationSystemProject.Models.Manager> Manager { get; set; } = default!;
        public DbSet<HotelReservationSystemProject.Models.Payment> Payment { get; set; } = default!;
        public DbSet<HotelReservationSystemProject.Models.Receptionist> Receptionist { get; set; } = default!;
        public DbSet<HotelReservationSystemProject.Models.Report> Report { get; set; } = default!;
        public DbSet<HotelReservationSystemProject.Models.Room> Room { get; set; } = default!;
        public DbSet<HotelReservationSystemProject.Models.RoomBooking> RoomBooking { get; set; } = default!;
        public DbSet<HotelReservationSystemProject.Models.RoomBookingDetails> RoomBookingDetails { get; set; } = default!;
        public DbSet<HotelReservationSystemProject.Models.RoomItems> RoomItems { get; set; } = default!;
        public DbSet<HotelReservationSystemProject.Models.Invoice> Invoice { get; set; } = default!;
    }
}
