using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Healthcare.Models;

public class MedicalRecord
{
    public int MedicalRecordID { get; set; }

    public required string Diagnosis { get; set; }
    
    public required string Treatment { get; set; }
    
    public DateTime RecordDate { get; set; }
    
    public int PatientID { get; set; }
    [ForeignKey("PatientID")]
    public Patient? Patient { get; set; }
}

