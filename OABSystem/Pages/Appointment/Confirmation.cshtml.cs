using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OABSystem.Data;
using OABSystem.Models;

namespace OABSystem.Pages.Appointment
{
    public class ConfirmationModel : PageModel
    {
        private readonly OABSystem.Data.OABSystemContext _context;

        public ConfirmationModel(OABSystem.Data.OABSystemContext context)
        {
            _context = context;
        }
        


        [BindProperty]
        public OABSystem.Models.Appointment Appointment { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnGetsync(int id)
        {
            Appointment = await _context.Appointment.Include(e=>e.HealthcareProfessional).FirstOrDefaultAsync(e=>e.AppointmentId ==id);
            
            

            return Page();
        }
        [ActionName("Get")]
        public IActionResult OnGet(int id)
        {
            return OnGetsync(id).Result;
        }
    }
}
