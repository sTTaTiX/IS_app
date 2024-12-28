using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Healthcare.Models;

public class Appointment
{
    public int AppointmentID { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Reason { get; set; } = string.Empty;

    [Required]
    public int PatientID { get; set; }

    [Required]
    public int DoctorID { get; set; }

    [ForeignKey("PatientID")]
    public Patient? Patient { get; set; }

    [ForeignKey("DoctorID")]
    public Doctor? Doctor { get; set; }
}
