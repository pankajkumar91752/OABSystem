﻿using System;
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

    public class IndexModel : PageModel
    {
        private readonly OABSystem.Data.OABSystemContext _context;

        public IndexModel(OABSystem.Data.OABSystemContext context)
        {
            _context = context;
        }

        public IList<HealthcareProfessional> HealthcareProfessional { get;set; } = default!;

       
        public async Task OnGetAsync()
        {
            if (_context.HealthcareProfessional != null)
            {
                HealthcareProfessional = await _context.HealthcareProfessional.ToListAsync();
            }
        }
    }
}
