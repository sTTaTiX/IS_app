using Healthcare.Data;
using Healthcare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareApp.Pages_MedicalRecords
{
    public class CreateModel : PageModel
    {
        private readonly HealthcareContext _context;

        public CreateModel(HealthcareContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MedicalRecord MedicalRecord { get; set; }

        public SelectList Patients { get; set; }

        public IActionResult OnGet()
        {
            // Populate the dropdown list with patient options
            Patients = new SelectList(
                _context.Patients.Select(p => new
                {
                    p.PatientID,
                    FullName = $"{p.FirstName} {p.LastName} ({p.PatientID})"
                }),
                "PatientID",
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

            _context.MedicalRecords.Add(MedicalRecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
