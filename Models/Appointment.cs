namespace Healthcare.Models;

public class Appointment
{
    public int AppointmentID { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Reason { get; set; } = string.Empty;

    public int PatientID { get; set; }
    public Patient Patient { get; set; } = null!;

    public int DoctorID { get; set; }
    public Doctor Doctor { get; set; } = null!;
}
