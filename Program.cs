using Healthcare.Data;
using Healthcare.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Set up the connection string for the database
var connectionString = builder.Configuration.GetConnectionString("HealthcareContext");

// Add services to the container
builder.Services.AddControllersWithViews();

// Configure DbContext for HealthcareContext
builder.Services.AddDbContext<HealthcareContext>(options =>
    options.UseSqlServer(connectionString));

// Configure Identity with roles
builder.Services.AddDefaultIdentity<HealthcareUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>() // Enable roles
    .AddEntityFrameworkStores<HealthcareContext>(); // Link to your DbContext

// Build the application
var app = builder.Build();

// Seed the database with initial data, including roles
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<HealthcareContext>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await SeedData.InitializeAsync(context, roleManager); // Call a method to seed data
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

// Configure HTTP request pipeline
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

app.MapRazorPages(); // For user login and registration (if using Razor Pages)

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
