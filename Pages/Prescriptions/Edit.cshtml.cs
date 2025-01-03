using Healthcare.Data;
using Healthcare.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareApp.Pages_Prescriptions
{
    [Authorize(Roles="Administrator, Doctor")]
    public class EditModel : PageModel
    {
        private readonly HealthcareContext _context;

        public EditModel(HealthcareContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Prescription Prescription { get; set; }

        public SelectList Patients { get; set; }
        public SelectList Doctors { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Prescription = await _context.Prescriptions
                .FirstOrDefaultAsync(m => m.PrescriptionID == id);

            if (Prescription == null)
            {
                return NotFound();
            }

            // Populate the dropdown lists with patient and doctor options
            Patients = new SelectList(
                _context.Patients.Select(p => new
                {
                    p.PatientID,
                    FullName = $"{p.FirstName} {p.LastName} ({p.PatientID})"
                }),
                "PatientID",
                "FullName", 
                Prescription.PatientID
            );

            Doctors = new SelectList(
                _context.Doctors.Select(d => new
                {
                    d.DoctorID,
                    FullName = $"{d.FirstName} {d.LastName} ({d.DoctorID})"
                }),
                "DoctorID",
                "FullName", 
                Prescription.DoctorID
            );

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var prescriptionToUpdate = await _context.Prescriptions
                .FirstOrDefaultAsync(m => m.PrescriptionID == id);

            if (prescriptionToUpdate == null)
            {
                return NotFound();
            }

            prescriptionToUpdate.Medication = Prescription.Medication;
            prescriptionToUpdate.Dosage = Prescription.Dosage;
            prescriptionToUpdate.DateIssued = Prescription.DateIssued;
            prescriptionToUpdate.DoctorID = Prescription.DoctorID;
            prescriptionToUpdate.PatientID = Prescription.PatientID;

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
