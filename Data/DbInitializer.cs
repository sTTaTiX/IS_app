using Healthcare.Models;  // Uporaba Healthcare.Models za HealthcareUser
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Healthcare.Data
{
    public static class DbInitializer
    {
        public static void Initialize(HealthcareContext context)
        {
            context.Database.EnsureCreated();

            // Preveri, če baza že vsebuje paciente
            if (context.Patients.Any())
            {
                return; // Podatki že obstajajo
            }

            // Dodajanje pacientov
            var patients = new Patient[]
            {
                new Patient{FirstName="Carson", LastName="Alexander", DateOfBirth=DateTime.Parse("1990-09-01")},
                new Patient{FirstName="Meredith", LastName="Alonso", DateOfBirth=DateTime.Parse("1985-06-21")},
                new Patient{FirstName="Arturo", LastName="Anand", DateOfBirth=DateTime.Parse("1978-11-03")},
                new Patient{FirstName="Gytis", LastName="Barzdukas", DateOfBirth=DateTime.Parse("1992-02-13")},
                new Patient{FirstName="Yan", LastName="Li", DateOfBirth=DateTime.Parse("1982-07-07")}
            };
            context.Patients.AddRange(patients);
            context.SaveChanges();

            // Dodajanje zdravnikov
            var doctors = new Doctor[]
            {
                new Doctor{FirstName="John", LastName="Smith", Specialty="Cardiologist", ContactNumber="123456789"},
                new Doctor{FirstName="Anna", LastName="Jones", Specialty="Dermatologist", ContactNumber="987654321"}
            };
            context.Doctors.AddRange(doctors);
            context.SaveChanges();

            // Dodajanje srečanj (appointments)
            var appointments = new Appointment[]
            {
                new Appointment{PatientID=patients[0].PatientID, DoctorID=doctors[0].DoctorID, AppointmentDate=DateTime.Parse("2024-11-28"), Reason="Checkup"},
                new Appointment{PatientID=patients[1].PatientID, DoctorID=doctors[1].DoctorID, AppointmentDate=DateTime.Parse("2024-11-29"), Reason="Skin Issue"},
                new Appointment{PatientID=patients[2].PatientID, DoctorID=doctors[0].DoctorID, AppointmentDate=DateTime.Parse("2024-11-30"), Reason="Heart Check"},
                new Appointment{PatientID=patients[3].PatientID, DoctorID=doctors[1].DoctorID, AppointmentDate=DateTime.Parse("2024-12-01"), Reason="Rash Treatment"},
                new Appointment{PatientID=patients[4].PatientID, DoctorID=doctors[0].DoctorID, AppointmentDate=DateTime.Parse("2024-12-02"), Reason="Blood Pressure"}
            };
            context.Appointments.AddRange(appointments);
            context.SaveChanges();

            // Dodajanje vlog (roles) za Identity
            var roles = new IdentityRole[]
            {
                new IdentityRole{Id="1", Name="Administrator", NormalizedName="ADMINISTRATOR"},
                new IdentityRole{Id="2", Name="Manager", NormalizedName="MANAGER"},
                new IdentityRole{Id="3", Name="Staff", NormalizedName="STAFF"}
            };
            context.Roles.AddRange(roles);
            context.SaveChanges();

            // Ustvarjanje uporabnika (HealthcareUser)
            var user = new HealthcareUser
            {
                FirstName = "Bob",
                LastName = "Dilon",
                City = "Ljubljana",
                Email = "bob@example.com",
                NormalizedEmail = "BOB@EXAMPLE.COM",
                UserName = "bob@example.com",
                NormalizedUserName = "BOB@EXAMPLE.COM",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<HealthcareUser>();
                user.PasswordHash = password.HashPassword(user, "Testni123!");
                context.Users.Add(user);
            }
            context.SaveChanges();

            // Dodeljevanje vlog uporabniku
            var userRoles = new IdentityUserRole<string>[]
            {
                new IdentityUserRole<string>{RoleId = roles[0].Id, UserId=user.Id},
                new IdentityUserRole<string>{RoleId = roles[1].Id, UserId=user.Id}
            };
            context.UserRoles.AddRange(userRoles);
            context.SaveChanges();
        }
    }
}
