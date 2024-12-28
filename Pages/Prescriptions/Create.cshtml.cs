using Healthcare.Data;
using Healthcare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareApp.Pages_Prescriptions
{
    public class CreateModel : PageModel
    {
        private readonly HealthcareContext _context;

        public CreateModel(HealthcareContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Prescription Prescription { get; set; }

        public SelectList Patients { get; set; }
        public SelectList Doctors { get; set; }

        public IActionResult OnGet()
        {
            // Populate the dropdown lists with patient and doctor options
            Patients = new SelectList(
                _context.Patients.Select(p => new
                {
                    p.PatientID,
                    FullName = $"{p.FirstName} {p.LastName} ({p.PatientID})"
                }),
                "PatientID",
                "FullName"
            );

            Doctors = new SelectList(
                _context.Doctors.Select(d => new
                {
                    d.DoctorID,
                    FullName = $"{d.FirstName} {d.LastName} ({d.DoctorID})"
                }),
                "DoctorID",
                "FullName"
            );

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Prescriptions.Add(Prescription);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
