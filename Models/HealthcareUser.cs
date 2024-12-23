using Microsoft.AspNetCore.Identity;

namespace Healthcare.Models;

public class HealthcareUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}
