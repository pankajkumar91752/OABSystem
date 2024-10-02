using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OABSystem.Data;
using OABSystem.Models;

namespace OABSystem.Pages.Appointment
{
    public class DetailsModel : PageModel
    {
        private readonly OABSystem.Data.OABSystemContext _context;

        public DetailsModel(OABSystem.Data.OABSystemContext context)
        {
            _context = context;
        }

      public OABSystem.Models. Appointment Appointment { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Appointment == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment.Include(e => e.HealthcareProfessional).FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }
            else 
            {
                Appointment = appointment;
            }
            return Page();
        }
    }
}
