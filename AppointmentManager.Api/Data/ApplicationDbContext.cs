using AppointmentManager.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentManager.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<User> Users => Set<User>();
    }
}
