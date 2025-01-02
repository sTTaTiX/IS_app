using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Healthcare.Models;
using Microsoft.AspNetCore.Authorization;

namespace HealthcareApp.Pages_Doctors
{
    [Authorize(Roles="Administrator")]
    public class EditModel : PageModel
    {
        private readonly Healthcare.Data.HealthcareContext _context;

        public EditModel(Healthcare.Data.HealthcareContext context)
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

            var doctor =  await _context.Doctors.FirstOrDefaultAsync(m => m.DoctorID == id);
            if (doctor == null)
            {
                return NotFound();
            }
            Doctor = doctor;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Doctor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(Doctor.DoctorID))
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

        private bool DoctorExists(int id)
        {
            return _context.Doctors.Any(e => e.DoctorID == id);
        }
    }
}
