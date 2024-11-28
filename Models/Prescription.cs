namespace Healthcare.Models;

public class Prescription
{
    public int PrescriptionID { get; set; }
    public string Medication { get; set; } = string.Empty;
    public string Dosage { get; set; } = string.Empty;
    public DateTime DateIssued { get; set; }
    
    public int DoctorID { get; set; }
    public Doctor Doctor { get; set; } = null!;
    
    public int PatientID { get; set; }
    public Patient Patient { get; set; } = null!;
}
