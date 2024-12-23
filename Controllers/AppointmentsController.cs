using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Healthcare.Models;
using Healthcare.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Healthcare.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AppointmentsController : Controller
    {
        private readonly HealthcareContext _context;

        public AppointmentsController(HealthcareContext context)
        {
            _context = context;
        }

        // GET: Appointments
        /*public async Task<IActionResult> Index()
        {
            // Poiščemo prvi appointment z vključitvijo pacientov in zdravnikov
            if (_context.Appointments == null)
            {
                return NotFound("Appointments data not found");
            }

            var appointment = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync();  // Uporabite FirstOrDefaultAsync() za en appointment

            // Preverite, če appointment ali njegovi povezani objekti niso null
            if (appointment == null || appointment.Patient == null || appointment.Doctor == null)
            {
                // Lahko preusmerite uporabnika ali prikažete napako
                return NotFound("Appointment or related data not found");
            }

            // Če so podatki v redu, pošljite appointment v pogled
            return View(appointment);
        }
        */
        public async Task<IActionResult> Index()
        {
            if (_context.Appointments == null)
            {
                return NotFound("Appointments data not found");
            }
            return View(await _context.Appointments.ToListAsync());
            // Passes the list to the view
        }



        // GET: Appointments/Create
        public IActionResult Create()
        {
            ViewBag.Patients = new SelectList(_context.Patients, "PatientID", "Name");  // PatientID kot vrednost, Name kot prikazano besedilo
            ViewBag.Doctors = new SelectList(_context.Doctors, "DoctorID", "Name");  // DoctorID kot vrednost, Name kot prikazano besedilo
            return View();
        }

        // POST: Appointments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentID,PatientID,DoctorID,AppointmentDate,Reason")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(m => m.AppointmentID == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
