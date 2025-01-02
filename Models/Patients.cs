using Healthcare.Data;
namespace Healthcare.Models
{
    public class Patient
    {
        public int PatientID { get; set; }


        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}";

    }
}
