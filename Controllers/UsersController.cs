// using System;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using StoreApp.Models;

// using StoreApp.Data;
// using Microsoft.EntityFrameworkCore;

// namespace StoreApp.Controllers
// {
//     public class UsersController : Controller
//     {
//         private readonly StoreAppContext _context;

//         public UsersController(StoreAppContext context)
//         {
//             _context = context;
//         }
//         public async Task<IActionResult> Index()
//         {
//             return View(await _context.Users.ToListAsync());
//         }

//         public async Task<IActionResult> Details(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var user = await _context.Users
//                 .Include(s => s.UserPermissions)
//                     .ThenInclude(u => u.Permission)
//                 .AsNoTracking()
//                 .SingleOrDefaultAsync(m => m.ID == id);

//             //Console.WriteLine(user);
            
//             // var user = await _context.Users
//             //     .SingleOrDefaultAsync(m = m.ID == id);

//             if (user == null)
//             {
//                 return NotFound();
//             }

//             return View(user);
//         }

//         public IActionResult Error()
//         {
//             return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//         }
//     }
// }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data;
using StoreApp.Models;
 
namespace StoreApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly StoreAppContext _context;
 
        public UsersController(StoreAppContext context)
        {
            _context = context;
        }
 
        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }
 
        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
 
            var user = await _context.Users
                .Include(u => u.UserPermissions)
                    .ThenInclude(p => p.Permission)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }
 
            return View(user);
        }
 
        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }
 
        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Address,Nationality")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
 
        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
 
            var user = await _context.Users.SingleOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
 
        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Address,Nationality")] User user)
        {
            if (id != user.ID)
            {
                return NotFound();
            }
 
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
 
        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
 
            var user = await _context.Users
                .SingleOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }
 
            return View(user);
        }
 
        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(m => m.ID == id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
 
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
    }
}