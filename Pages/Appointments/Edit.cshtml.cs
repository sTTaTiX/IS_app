using Healthcare.Data;
using Healthcare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareApp.Pages_Appointments
{
    public class EditModel : PageModel
    {
        private readonly HealthcareContext _context;

        public EditModel(HealthcareContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Appointment Appointment { get; set; }

        public SelectList Patients { get; set; }
        public SelectList Doctors { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Appointment = await _context.Appointments.FindAsync(id);

            if (Appointment == null)
            {
                return NotFound();
            }

            Patients = new SelectList(
                _context.Patients.Select(p => new 
                { 
                    p.PatientID, 
                    FullName = $"{p.FirstName} {p.LastName} ({p.PatientID})" 
                }),
                "PatientID",
                "FullName",
                Appointment.PatientID
            );

            Doctors = new SelectList(
                _context.Doctors.Select(d => new 
                { 
                    d.DoctorID, 
                    FullName = $"{d.FirstName} {d.LastName} ({d.DoctorID})" 
                }),
                "DoctorID",
                "FullName",
                Appointment.DoctorID
            );

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Appointment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
            {
                if (!_context.Appointments.Any(a => a.AppointmentID == Appointment.AppointmentID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
