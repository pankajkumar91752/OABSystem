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
using OABSystem.Models;

namespace OABSystem.Views.Appointment
{
    [Authorize(Roles = "Admin")]

    public class EditModel : PageModel
    {
        private readonly OABSystem.Data.OABSystemContext _context;

        public EditModel(OABSystem.Data.OABSystemContext context)
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

            var healthcareprofessional =  await _context.HealthcareProfessional.FirstOrDefaultAsync(m => m.Id == id);
            if (healthcareprofessional == null)
            {
                return NotFound();
            }
            HealthcareProfessional = healthcareprofessional;
            return Page();
        }
        [Authorize(Roles = "Admin")]


        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(HealthcareProfessional).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HealthcareProfessionalExists(HealthcareProfessional.Id))
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

        private bool HealthcareProfessionalExists(int id)
        {
          return (_context.HealthcareProfessional?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
