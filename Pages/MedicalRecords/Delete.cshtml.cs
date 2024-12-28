using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Healthcare.Models;
using Microsoft.AspNetCore.Authorization;

namespace HealthcareApp.Pages_MedicalRecords
{   
    [Authorize(Roles="Administrator, Doctor")]
    public class DeleteModel : PageModel
    {
        private readonly Healthcare.Models.HealthcareContext _context;

        public DeleteModel(Healthcare.Models.HealthcareContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MedicalRecord MedicalRecord { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalrecord = await _context.MedicalRecords.FirstOrDefaultAsync(m => m.MedicalRecordID == id);

            if (medicalrecord == null)
            {
                return NotFound();
            }
            else
            {
                MedicalRecord = medicalrecord;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalrecord = await _context.MedicalRecords.FindAsync(id);
            if (medicalrecord != null)
            {
                MedicalRecord = medicalrecord;
                _context.MedicalRecords.Remove(MedicalRecord);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
