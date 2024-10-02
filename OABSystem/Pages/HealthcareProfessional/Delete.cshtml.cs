using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OABSystem.Data;
using OABSystem.Models;

namespace OABSystem.Views.Appointment
{
    [Authorize(Roles = "Admin")]

    public class DeleteModel : PageModel
    {
        private readonly OABSystem.Data.OABSystemContext _context;

        public DeleteModel(OABSystem.Data.OABSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
      public HealthcareProfessional HealthcareProfessional { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.HealthcareProfessional == null)
            {
                return NotFound();
            }

            var healthcareprofessional = await _context.HealthcareProfessional.FirstOrDefaultAsync(m => m.Id == id);

            if (healthcareprofessional == null)
            {
                return NotFound();
            }
            else 
            {
                HealthcareProfessional = healthcareprofessional;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.HealthcareProfessional == null)
            {
                return NotFound();
            }
            var healthcareprofessional = await _context.HealthcareProfessional.FindAsync(id);

            if (healthcareprofessional != null)
            {
                HealthcareProfessional = healthcareprofessional;
                _context.HealthcareProfessional.Remove(HealthcareProfessional);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
