using System.ComponentModel.DataAnnotations.Schema;

namespace Healthcare.Models;

public class Prescription
{
    public int PrescriptionID { get; set; }
    public string Medication { get; set; } = string.Empty;
    public string Dosage { get; set; } = string.Empty;
    public DateTime DateIssued { get; set; }
    
    public int DoctorID { get; set; }
    
    public int PatientID { get; set; }
    [ForeignKey("PatientID")]
    public Patient? Patient { get; set; }

    [ForeignKey("DoctorID")]
    public Doctor? Doctor { get; set; }
}
