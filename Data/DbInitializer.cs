
using Healthcare.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
namespace Healthcare.Data;

public static class DbInitializer
{
    public static void Initialize(HealthcareContext context)
    {
        context.Database.EnsureCreated();

        // Preveri, če so v bazi že podatki o pacientih
        if (context.Patients.Any())
        {
            return; // DB je že inicializiran
        }

        // Dodaj začetne paciente
        var patients = new Patient[]
        {
            new Patient { FirstName = "John", LastName = "Doe", DateOfBirth = DateTime.Parse("1980-01-15"), Address = "123 Main St, Ljubljana", Email = "john.doe@example.com" },
            new Patient { FirstName = "Jane", LastName = "Smith", DateOfBirth = DateTime.Parse("1990-03-22"), Address = "456 Elm St, Maribor", Email = "jane.smith@example.com" },
            new Patient { FirstName = "Mark", LastName = "Johnson", DateOfBirth = DateTime.Parse("1975-07-08"), Address = "789 Pine St, Celje", Email = "mark.johnson@example.com" },
        };
        context.Patients.AddRange(patients);
        context.SaveChanges();

        // Dodaj začetne zdravnike
        var doctors = new Doctor[]
        {
            new Doctor { FirstName = "Emily", LastName = "Brown", Specialty = "Cardiology", ContactNumber = "+38640123456" },
            new Doctor { FirstName = "Michael", LastName = "Taylor", Specialty = "Neurology", ContactNumber = "+38640234567" },
            new Doctor { FirstName = "Sarah", LastName = "Wilson", Specialty = "Pediatrics", ContactNumber = "+38640345678" },
        };
        context.Doctors.AddRange(doctors);
        context.SaveChanges();

        // Dodaj začetne termine
        var appointments = new Appointment[]
        {
            new Appointment { AppointmentDate = DateTime.Now.AddDays(7), Reason = "Routine checkup", PatientID = 1, DoctorID = 1 },
            new Appointment { AppointmentDate = DateTime.Now.AddDays(10), Reason = "Follow-up visit", PatientID = 2, DoctorID = 2 },
            new Appointment { AppointmentDate = DateTime.Now.AddDays(14), Reason = "Special consultation", PatientID = 3, DoctorID = 3 },
        };
        context.Appointments.AddRange(appointments);
        context.SaveChanges();

        // Dodaj začetne zdravstvene kartoteke
        var medicalRecords = new MedicalRecord[]
        {
            new MedicalRecord { Diagnosis = "Hypertension", Treatment = "Medication A", RecordDate = DateTime.Now.AddMonths(-3), PatientID = 1 },
            new MedicalRecord { Diagnosis = "Diabetes", Treatment = "Medication B", RecordDate = DateTime.Now.AddMonths(-6), PatientID = 2 },
            new MedicalRecord { Diagnosis = "Asthma", Treatment = "Inhaler C", RecordDate = DateTime.Now.AddMonths(-12), PatientID = 3 },
        };
        context.MedicalRecords.AddRange(medicalRecords);
        context.SaveChanges();

        // Dodaj začetne recepte
        var prescriptions = new Prescription[]
        {
            new Prescription { Medication = "Drug A", Dosage = "10mg once daily", DateIssued = DateTime.Now.AddDays(-7), DoctorID = 1, PatientID = 1 },
            new Prescription { Medication = "Drug B", Dosage = "5mg twice daily", DateIssued = DateTime.Now.AddDays(-10), DoctorID = 2, PatientID = 2 },
            new Prescription { Medication = "Drug C", Dosage = "Use as needed", DateIssued = DateTime.Now.AddDays(-14), DoctorID = 3, PatientID = 3 },
        };
        context.Prescriptions.AddRange(prescriptions);
        context.SaveChanges();

        // Dodaj začetne vloge
        var roles = new IdentityRole[]
        {
            new IdentityRole { Id = "1", Name = "Administrator", NormalizedName = "ADMINISTRATOR" },
            new IdentityRole { Id = "2", Name = "Doctor", NormalizedName = "DOCTOR" },
            new IdentityRole { Id = "3", Name = "Patient", NormalizedName = "PATIENT" },
        };
        foreach (var role in roles)
        {
            context.Roles.Add(role);
        }
        context.SaveChanges();

        // Dodaj začetnega uporabnika
        var user = new ApplicationUser
        {
            
            Email = "admin@healthcare.com",
            NormalizedEmail = "ADMIN@HEALTHCARE.COM",
            UserName = "admin@healthcare.com",
            NormalizedUserName = "ADMIN@HEALTHCARE.COM",
            PhoneNumber = "+123456789",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString("D")
        };

        if (!context.Users.Any(u => u.UserName == user.UserName))
        {
            var password = new PasswordHasher<ApplicationUser>();
            var hashed = password.HashPassword(user, "Admin123!");
            user.PasswordHash = hashed;
            context.Users.Add(user);
        }
        context.SaveChanges();

        // Poveži uporabnika z vlogami
        var userRoles = new IdentityUserRole<string>[]
        {
            new IdentityUserRole<string> { RoleId = roles[0].Id, UserId = user.Id },
        };
        foreach (var userRole in userRoles)
        {
            context.UserRoles.Add(userRole);
        }
        context.SaveChanges();
    }
}