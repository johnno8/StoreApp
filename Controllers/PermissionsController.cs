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
//     public class PermissionsController : Controller
//     {
//         private readonly StoreAppContext _context;

//         public PermissionsController(StoreAppContext context)
//         {
//             _context = context;
//         }
//         public async Task<IActionResult> Index()
//         {
//             return View(await _context.Permissions.ToListAsync());
//         }

//         public IActionResult Error()
//         {
//             return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//         }
//     }
// }

﻿using System;
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
    public class PermissionsController : Controller
    {
        private readonly StoreAppContext _context;
 
        public PermissionsController(StoreAppContext context)
        {
            _context = context;
        }
 
        // GET: Permissions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Permissions.ToListAsync());
        }
 
        // GET: Permissions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
 
            var permission = await _context.Permissions
                .SingleOrDefaultAsync(m => m.PermissionID == id);
            if (permission == null)
            {
                return NotFound();
            }
 
            return View(permission);
        }
 
        // GET: Permissions/Create
        public IActionResult Create()
        {
            return View();
        }
 
        // POST: Permissions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PermissionID,Title,Description")] Permission permission)
        {
            if (ModelState.IsValid)
            {
                _context.Add(permission);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(permission);
        }
 
        // GET: Permissions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
 
            var permission = await _context.Permissions.SingleOrDefaultAsync(m => m.PermissionID == id);
            if (permission == null)
            {
                return NotFound();
            }
            return View(permission);
        }
 
        // POST: Permissions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PermissionID,Title,Description")] Permission permission)
        {
            if (id != permission.PermissionID)
            {
                return NotFound();
            }
 
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(permission);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PermissionExists(permission.PermissionID))
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
            return View(permission);
        }
 
        // GET: Permissions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
 
            var permission = await _context.Permissions
                .SingleOrDefaultAsync(m => m.PermissionID == id);
            if (permission == null)
            {
                return NotFound();
            }
 
            return View(permission);
        }
 
        // POST: Permissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var permission = await _context.Permissions.SingleOrDefaultAsync(m => m.PermissionID == id);
            _context.Permissions.Remove(permission);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
 
        private bool PermissionExists(int id)
        {
            return _context.Permissions.Any(e => e.PermissionID == id);
        }
    }
}