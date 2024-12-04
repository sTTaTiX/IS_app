using Healthcare.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class HealthcareContext(DbContextOptions<HealthcareContext> options) : IdentityDbContext<HealthcareUser>(options)
{

    public required DbSet<Patient> Patients { get; set; }
    public required DbSet<Doctor> Doctors { get; set; }
    public required DbSet<Appointment> Appointments { get; set; }
    public required DbSet<MedicalRecord> MedicalRecords { get; set; }
    public required DbSet<Prescription> Prescriptions { get; set; }

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
