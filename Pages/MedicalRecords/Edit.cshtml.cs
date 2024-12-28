using Healthcare.Data;
using Healthcare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareApp.Pages_MedicalRecords
{
    public class EditModel : PageModel
    {
        private readonly HealthcareContext _context;

        public EditModel(HealthcareContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MedicalRecord MedicalRecord { get; set; }

        public SelectList Patients { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            MedicalRecord = await _context.MedicalRecords
                .FirstOrDefaultAsync(m => m.MedicalRecordID == id);

            if (MedicalRecord == null)
            {
                return NotFound();
            }

            // Populate the dropdown list with patient options
            Patients = new SelectList(
                _context.Patients.Select(p => new
                {
                    p.PatientID,
                    FullName = $"{p.FirstName} {p.LastName} ({p.PatientID})"
                }),
                "PatientID",
                "FullName",
                MedicalRecord.PatientID
            );

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var medicalRecordToUpdate = await _context.MedicalRecords
                .FirstOrDefaultAsync(m => m.MedicalRecordID == id);

            if (medicalRecordToUpdate == null)
            {
                return NotFound();
            }

            medicalRecordToUpdate.Diagnosis = MedicalRecord.Diagnosis;
            medicalRecordToUpdate.Treatment = MedicalRecord.Treatment;
            medicalRecordToUpdate.RecordDate = MedicalRecord.RecordDate;
            medicalRecordToUpdate.PatientID = MedicalRecord.PatientID;

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
