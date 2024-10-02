using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OABSystem.Data;
using OABSystem.Models;

namespace OABSystem.Pages.Appointment
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly OABSystem.Data.OABSystemContext _context;

        public CreateModel(OABSystem.Data.OABSystemContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id = null)
        {
            if (id == null)
            {
                HealthProfessionals = new SelectList(await _context.HealthcareProfessional?.ToListAsync(), "Id", "Name");
                return Page();
            }
            else
            {
                Appointment = await _context.Appointment.Include(e => e.HealthcareProfessional).FirstOrDefaultAsync(e => e.AppointmentId == id);



                return Page();
            }



        }


        [BindProperty]
        public OABSystem.Models.Appointment Appointment { get; set; } = default!;
        public IEnumerable<SelectListItem>? HealthProfessionals { get; private set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
           
                Appointment.HealthcareProfessional = await _context.HealthcareProfessional.Include(e => e.Appointments).FirstOrDefaultAsync(e => e.Id == Appointment.HealthcareProfessional.Id);
                var v = Appointment.ValidateAppointmentDateTime().Where(e => !string.IsNullOrEmpty(e?.ErrorMessage));
                if (v.Any())
                    ModelState.AddModelError("Appointment.AppointmentDateTime", string.Join(",", v.Select(o => o.ErrorMessage)));
                ModelState.Remove("Appointment.HealthcareProfessional.Name");
                if (!ModelState.IsValid || _context.Appointment == null || Appointment == null)
                {
                    HealthProfessionals = new SelectList(await _context.HealthcareProfessional?.ToListAsync(), "Id", "Name");

                    return Page();
                }

                _context.Appointment.Add(Appointment);
               // Appointment = new Models.Appointment();
                await _context.SaveChangesAsync();

                //return RedirectToPage("Confirmation", new  { id= Appointment.AppointmentId});
                return RedirectToPage("Create", new { id = Appointment.AppointmentId });
            
           
        }
    }
}
