namespace Healthcare.Models;

public class Doctor
{
    public int DoctorID { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Specialty { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
}
