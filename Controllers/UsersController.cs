using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Models;

using StoreApp.Data;
using Microsoft.EntityFrameworkCore;

namespace StoreApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly StoreAppContext _context;

        public UsersController(StoreAppContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(s => s.UserPermissions)
                    .ThenInclude(u => u.Permission)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);

            //Console.WriteLine(user);
            
            // var user = await _context.Users
            //     .SingleOrDefaultAsync(m = m.ID == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}