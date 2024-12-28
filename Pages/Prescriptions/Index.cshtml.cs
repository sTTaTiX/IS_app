using Healthcare.Data;
using Healthcare.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HealthcareApp.Pages_Prescriptions
{
    public class IndexModel : PageModel
    {
        private readonly HealthcareContext _context;

        public IndexModel(HealthcareContext context)
        {
            _context = context;
        }

        public IList<Prescription> Prescription { get; set; }

        public async Task OnGetAsync()
        {
            Prescription = await _context.Prescriptions
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .ToListAsync();
        }
    }
}
