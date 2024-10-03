using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OABSystem.Areas.Identity.Data;
using OABSystem.Data;
using OABSystem.Models;

namespace OABSystem.Pages.Appointment
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly OABSystem.Data.OABSystemContext _context;
        private readonly UserManager<OABSystemUser> userManager;

        public IndexModel(OABSystem.Data.OABSystemContext context, UserManager<OABSystemUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        public IList<OABSystem.Models.Appointment> Appointment { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var isadmin  = await userManager.IsInRoleAsync(user,"Admin");
            if (_context.Appointment != null)
            {
                Appointment = await _context.Appointment.Where(e=>isadmin || e.UserName == user.UserName).Include(e=>e.HealthcareProfessional).ToListAsync();
            }
        }
    }
}
