using Healthcare.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Healthcare.Data
{

    public class HealthcareContext : IdentityDbContext<HealthcareUser>
    {
        public HealthcareContext(DbContextOptions<HealthcareContext> options) : base(options) { }

        public DbSet<Patient>? Patients { get; set; }
        public DbSet<Doctor>? Doctors { get; set; }
        public DbSet<Appointment>? Appointments { get; set; }
        public DbSet<MedicalRecord>? MedicalRecords { get; set; }
        public DbSet<Prescription>? Prescriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);  // Important to call base method for Identity setup

            modelBuilder.Entity<Patient>().ToTable("Patient");
            modelBuilder.Entity<Doctor>().ToTable("Doctor");
            modelBuilder.Entity<Appointment>().ToTable("Appointment");
            modelBuilder.Entity<MedicalRecord>().ToTable("MedicalRecord");
            modelBuilder.Entity<Prescription>().ToTable("Prescription");
        }
    }
}