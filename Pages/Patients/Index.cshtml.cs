using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Healthcare.Models;
using Microsoft.AspNetCore.Authorization;
using Healthcare.Data;
namespace HealthcareApp.Pages_Patients
{
    [Authorize(Roles="Administrator")]
    public class IndexModel : PageModel
    {
        private readonly Healthcare.Data.HealthcareContext _context;

        public IndexModel(Healthcare.Data.HealthcareContext context)
        {
            _context = context;
        }

        public IList<Patient> Patient { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Patient = await _context.Patients.AsNoTracking().ToListAsync();
        }
        public async Task<JsonResult> OnGetPatientsJsonAsync()
        {
            var patients = await _context.Patients.AsNoTracking().ToListAsync();
            return new JsonResult(patients);
        }
    }
}
