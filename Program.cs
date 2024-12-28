using Healthcare.Data;
using Healthcare.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Nastavi povezavo do baze podatkov
var connectionString = builder.Configuration.GetConnectionString("HealthcareContext") ?? throw new InvalidOperationException("Connection string 'HealthcareContext' not found.");

// Dodaj storitve za EF Core in Identity
builder.Services.AddDbContext<HealthcareContext>(options =>
    options.UseSqlServer(connectionString)); // Poveži se z SQL Serverjem

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>() // Dodaj podporo za vloge
    .AddEntityFrameworkStores<HealthcareContext>(); // Poveži se z našim kontekstom podatkov

// Dodaj MVC in Razor Pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Inicializiraj bazo podatkov z DbInitializer
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<HealthcareContext>();
    DbInitializer.Initialize(context); // Inicializacija baze podatkov
}

// Nastavi konfiguracijo HTTP zahtev
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Privzeta vrednost HSTS je 30 dni
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Dodaj avtentifikacijo
app.UseAuthorization();  // Dodaj avtorizacijo

// Dodaj usmeritve za Razor Pages in kontrolerje
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
