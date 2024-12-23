namespace Healthcare.Models;

public class MedicalRecord
{
    public int MedicalRecordID { get; set; }
    public string Diagnosis { get; set; } = string.Empty;
    public string Treatment { get; set; } = string.Empty;
    public DateTime RecordDate { get; set; }
    
    public int PatientID { get; set; }
    public Patient Patient { get; set; } = null!;
}
