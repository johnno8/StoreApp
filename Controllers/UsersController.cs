using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Models;

//using Microsoft.Extensions.DependencyInjection;
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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}