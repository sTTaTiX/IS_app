using Healthcare.Data;
using Healthcare.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HealthcareApp.Pages_MedicalRecords
{
    public class IndexModel : PageModel
    {
        private readonly HealthcareContext _context;

        public IndexModel(HealthcareContext context)
        {
            _context = context;
        }

        public IList<MedicalRecord> MedicalRecord { get; set; }

        public async Task OnGetAsync()
        {
            MedicalRecord = await _context.MedicalRecords
                .Include(a => a.Patient)
                .ToListAsync();
        }
    }
}
