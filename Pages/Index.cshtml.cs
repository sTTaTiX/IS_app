using Healthcare.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthcareApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(ILogger<IndexModel> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public List<string> UserRoles { get; set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);  // Get the current logged-in user
            if (user != null)
            {
                UserRoles = new List<string>(await _userManager.GetRolesAsync(user));  // Get roles assigned to the user
            }
        }
    }
}
