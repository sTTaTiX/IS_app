using Healthcare.Data;
using Healthcare.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareApp.Pages_Appointments
{
    public class IndexModel : PageModel
    {
        private readonly HealthcareContext _context;

        public IndexModel(HealthcareContext context)
        {
            _context = context;
        }

        public IList<Appointment> Appointment { get; set; }

        public async Task OnGetAsync()
        {
            Appointment = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .ToListAsync();
        }
    }
}
