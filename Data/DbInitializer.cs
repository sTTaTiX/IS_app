using Healthcare.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace Healthcare.Data
{
    public static class DbInitializer
    {
        public static void Initialize(HealthcareContext context)
        {
            context.Database.EnsureCreated();

            if (context.Patients.Any())
            {
                return; // Podatki že obstajajo
            }

            // Dodajanje pacientov
            var patients = new Patient[]
            {
                new Patient { FirstName = "Carson", LastName = "Alexander", DateOfBirth = DateTime.Parse("1990-09-01"), Address = "123 Main St", Email = "carson.alexander@example.com" },
                new Patient { FirstName = "Meredith", LastName = "Alonso", DateOfBirth = DateTime.Parse("1985-06-21"), Address = "456 Elm St", Email = "meredith.alonso@example.com" },
                new Patient { FirstName = "Arturo", LastName = "Anand", DateOfBirth = DateTime.Parse("1978-11-03"), Address = "789 Pine St", Email = "arturo.anand@example.com" }
            };
            context.Patients.AddRange(patients);
            context.SaveChanges();

            // Dodajanje zdravnikov
            var doctors = new Doctor[]
            {
                new Doctor { FirstName = "John", LastName = "Doe", Specialty = "Cardiology", ContactNumber = "123-456-7890" },
                new Doctor { FirstName = "Jane", LastName = "Smith", Specialty = "Dermatology", ContactNumber = "987-654-3210" },
                new Doctor { FirstName = "Emily", LastName = "Johnson", Specialty = "Pediatrics", ContactNumber = "555-555-5555" }
            };
            context.Doctors.AddRange(doctors);
            context.SaveChanges();

            // Dodajanje terminov
            var appointments = new Appointment[]
            {
                new Appointment { AppointmentDate = DateTime.Now.AddDays(1), Reason = "Routine Checkup", PatientID = patients[0].PatientID, DoctorID = doctors[0].DoctorID },
                new Appointment { AppointmentDate = DateTime.Now.AddDays(2), Reason = "Skin Rash", PatientID = patients[1].PatientID, DoctorID = doctors[1].DoctorID },
                new Appointment { AppointmentDate = DateTime.Now.AddDays(3), Reason = "Child Fever", PatientID = patients[2].PatientID, DoctorID = doctors[2].DoctorID }
            };
            context.Appointments.AddRange(appointments);
            context.SaveChanges();

            // Dodajanje vlog
            var roles = new IdentityRole[]
            {
                new IdentityRole { Name = "Administrator", NormalizedName = "ADMINISTRATOR" },
                new IdentityRole { Name = "Doctor", NormalizedName = "DOCTOR" },
                new IdentityRole { Name = "Patient", NormalizedName = "PATIENT" }
            };
            foreach (var role in roles)
            {
                if (!context.Roles.Any(r => r.Name == role.Name))
                {
                    context.Roles.Add(role);
                }
            }
            context.SaveChanges();

            // Ustvarjanje uporabnikov
            var users = new HealthcareUser[]
            {
                new HealthcareUser { FirstName = "Admin", LastName = "User", Email = "admin@admin.si", UserName = "admin@admin.si", EmailConfirmed = true },
                new HealthcareUser { FirstName = "Doctor", LastName = "User", Email = "doctor@zdravnik.si", UserName = "doctor@zdravnik.si", EmailConfirmed = true },
                new HealthcareUser { FirstName = "Patient", LastName = "User", Email = "patient@example.com", UserName = "patient@example.com", EmailConfirmed = true }
            };

            var passwordHasher = new PasswordHasher<HealthcareUser>();
            foreach (var user in users)
            {
                if (!context.Users.Any(u => u.UserName == user.UserName))
                {
                    user.PasswordHash = passwordHasher.HashPassword(user, "123!");
                    context.Users.Add(user);
                    context.SaveChanges();

                    // Dodeljevanje ustrezne vloge
                    string roleName = user.Email.Contains("@admin.si") ? "Administrator" :
                                      user.Email.Contains("@zdravnik.si") ? "Doctor" : "Patient";
                    var roleId = context.Roles.Single(r => r.Name == roleName).Id;

                    context.UserRoles.Add(new IdentityUserRole<string>
                    {
                        UserId = user.Id,
                        RoleId = roleId
                    });
                }
            }
            context.SaveChanges();
        }
    }
}
