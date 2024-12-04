using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Healthcare.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Healthcare.Controllers
{
    public class ProfileController : Controller
    {
        private readonly HealthcareContext _context;

        public ProfileController(HealthcareContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Pridobitev trenutnega uporabnika
            var userName = User.Identity.Name;

            // Pridobivanje vloge trenutnega uporabnika
            var userRoles = await _context.Users
                .Where(u => u.UserName == userName)
                .Join(_context.UserRoles, u => u.Id, ur => ur.UserId, (u, ur) => ur)
                .Join(_context.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => r.Name)
                .ToListAsync();

            // Združite vloge v en sam niz
            var rolesString = string.Join(", ", userRoles);

            // Prenesite vloge v pogled
            ViewData["UserRoles"] = rolesString;

            return View();
        }
    }
}
