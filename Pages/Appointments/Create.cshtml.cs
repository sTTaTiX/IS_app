using Healthcare.Data;
using Healthcare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareApp.Pages_Appointments
{
    public class CreateModel(HealthcareContext context) : PageModel
    {
        private readonly HealthcareContext _context = context;

        [BindProperty]
        public Appointment Appointment { get; set; }

        public SelectList Patients { get; set; }
        public SelectList Doctors { get; set; }

        public IActionResult OnGet()
        {
            Patients = new SelectList(
                _context.Patients.Select(p => new 
                { 
                    p.PatientID, 
                    FullName = $"{p.FullName} ({p.PatientID})" 
                }),
                "PatientID",
                "FullName"
            );

            Doctors = new SelectList(
                _context.Doctors.Select(d => new 
                { 
                    d.DoctorID, 
                    FullName = $"{d.FullName} ({d.DoctorID})" 
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

            _context.Appointments.Add(Appointment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
