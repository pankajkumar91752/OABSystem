using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OABSystem.Data;
using OABSystem.Models;

namespace OABSystem.Views.Appointment
{
    [Authorize(Roles = "Admin")]

    public class CreateModel : PageModel
    {
        private readonly OABSystem.Data.OABSystemContext _context;

        public CreateModel(OABSystem.Data.OABSystemContext context)
        {
            _context = context;
        }
        

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public HealthcareProfessional HealthcareProfessional { get; set; } = default!;

       // [Authorize(Roles = "Admin")]

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.HealthcareProfessional == null || HealthcareProfessional == null)
            {
                return Page();
            }

            _context.HealthcareProfessional.Add(HealthcareProfessional);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
