using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Healthcare.Models;
using Microsoft.AspNetCore.Authorization;

namespace HealthcareApp.Pages_Doctors
{
    [Authorize(Roles="Administrator")]
    public class DeleteModel : PageModel
    {
        private readonly Healthcare.Data.HealthcareContext _context;

        public DeleteModel(Healthcare.Data.HealthcareContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Doctor Doctor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors.FirstOrDefaultAsync(m => m.DoctorID == id);

            if (doctor == null)
            {
                return NotFound();
            }
            else
            {
                Doctor = doctor;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor != null)
            {
                Doctor = doctor;
                _context.Doctors.Remove(Doctor);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
