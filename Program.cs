using Healthcare.Data;
using Healthcare.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Nastavite spremenljivko connectionString za .UseSqlServer(connectionString)
var connectionString = builder.Configuration.GetConnectionString("HealthcareContext"); // Spremenite na HealthcareContext

// Dodajte storitve v zabojnik.
builder.Services.AddControllersWithViews();

// Nastavite DbContext za HealthcareContext
builder.Services.AddDbContext<HealthcareContext>(options =>
    options.UseSqlServer(connectionString)); // Poskrbite, da uporabljate pravilno povezavo

// Prilagodite dodajanje identitete za HealthcareUser, vključite vloge
builder.Services.AddDefaultIdentity<HealthcareUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>() // Dodajte vloge
    .AddEntityFrameworkStores<HealthcareContext>(); // Povežite z vašim DbContext

var app = builder.Build();

// Seed podatkovne baze s pomočjo DbInitializer
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<HealthcareContext>();
    DbInitializer.Initialize(context);
}

// Konfigurirajte HTTP zahteve
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages(); // Za zagotovitev prijave in registracije uporabnikov (če uporabljate Razor Pages)

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


//dotnet aspnet-codegenerator controller -name AppointmentController -m Healthcare.Models.Appointment -dc Healthcare.Data.HealthcareContext -udl -outDir Controllers

//dotnet aspnet-codegenerator controller -name PatientsController -m Healthcare.Models.Patient -dc Healthcare.Data.HealthcareContext -udl -outDir Controllers

//dotnet aspnet-codegenerator controller -name DoctorController -m Healthcare.Models.Doctor -dc Healthcare.Data.HealthcareContext -udl -outDir Controllers

//dotnet aspnet-codegenerator controller -name HealthcareUser -m Healthcare.Models.HealthcareUser -dc Healthcare.Data.HealthcareContext -udl -outDir Controllers