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
    public class PermissionsController : Controller
    {
        private readonly StoreAppContext _context;

        public PermissionsController(StoreAppContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Permissions.ToListAsync());
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}