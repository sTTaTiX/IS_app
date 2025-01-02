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
    [Authorize(Roles="Administrator, Doctor")]
    public class IndexModel : PageModel
    {
        private readonly Healthcare.Data.HealthcareContext _context;

        public IndexModel(Healthcare.Data.HealthcareContext context)
        {
            _context = context;
        }

        public IList<Doctor> Doctor { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Doctor = await _context.Doctors.ToListAsync();
        }
    }
}
