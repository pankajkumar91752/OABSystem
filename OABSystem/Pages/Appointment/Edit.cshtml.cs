using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OABSystem.Data;
using OABSystem.Filters;
using OABSystem.Models;

namespace OABSystem.Pages.Appointment
{
    [Authorize]
    [TypeFilter(typeof(UserAppoinmentAuthorizationFilter))]

    [ValidateAntiForgeryToken]
    public class EditModel : PageModel
    {
        private readonly OABSystem.Data.OABSystemContext _context;

        public EditModel(OABSystem.Data.OABSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OABSystem.Models.Appointment Appointment { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Appointment == null)
            {
                return NotFound();
            }

            var appointment =  await _context.Appointment.Include(e => e.HealthcareProfessional).FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }
            Appointment = appointment;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Appointment.HealthcareProfessional.Name");

            if (!ModelState.IsValid)
            {
                return Page();
            }

           Appointment.HealthcareProfessional = 
             await _context.HealthcareProfessional.FirstOrDefaultAsync(m => m.Id == Appointment.HealthcareProfessional.Id);

            _context.Entry(Appointment).Reference(p => p.HealthcareProfessional).IsModified = false;
            _context.Attach(Appointment).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(Appointment.AppointmentId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AppointmentExists(int id)
        {
          return (_context.Appointment?.Any(e => e.AppointmentId == id)).GetValueOrDefault();
        }
    }
}
