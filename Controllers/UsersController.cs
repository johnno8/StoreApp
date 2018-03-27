using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data;
using StoreApp.Models;
using StoreApp.Models.ViewModels;
 
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
        // public async Task<IActionResult> Edit(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }
 
        //     var user = await _context.Users.SingleOrDefaultAsync(m => m.ID == id);
        //     if (user == null)
        //     {
        //         return NotFound();
        //     }
        //     return View(user);
        // }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.UserPermissions)
                .ThenInclude(u => u.Permission)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }
            PopulateUserPermissionData(user);
            return View(user);
        }

        private void PopulateUserPermissionData(User user)
        {
            var allPermissions = _context.Permissions;
            var userspermissions = new HashSet<int>(user.UserPermissions.Select(p => p.PermissionID));
            var viewModel = new List<UserPermissionData>();
            foreach (var perm in allPermissions)
            {
                viewModel.Add(new UserPermissionData
                {
                    PermissionID = perm.PermissionID,
                    Title = perm.Title,
                    Assigned = userspermissions.Contains(perm.PermissionID)
                });
            }
            ViewData["Permissions"] = viewModel;
        }
 
        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Address,Nationality")] User user)
        // {
        //     if (id != user.ID)
        //     {
        //         return NotFound();
        //     }
 
        //     if (ModelState.IsValid)
        //     {
        //         try
        //         {
        //             _context.Update(user);
        //             await _context.SaveChangesAsync();
        //         }
        //         catch (DbUpdateConcurrencyException)
        //         {
        //             if (!UserExists(user.ID))
        //             {
        //                 return NotFound();
        //             }
        //             else
        //             {
        //                 throw;
        //             }
        //         }
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(user);
        // }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedPermissions)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userToUpdate = await _context.Users
                .Include(i => i.UserPermissions)
                    .ThenInclude(i => i.Permission)
                .SingleOrDefaultAsync(m => m.ID == id);

            if (await TryUpdateModelAsync<User>(
                userToUpdate,
                "",
                i => i.Name, i => i.Address, i => i.Nationality))
            {
                // if (String.IsNullOrWhiteSpace(userToUpdate.OfficeAssignment?.Location))
                // {
                //     userToUpdate.OfficeAssignment = null;
                // }
                UpdateUserPermissions(selectedPermissions, userToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateUserPermissions(selectedPermissions, userToUpdate);
            PopulateUserPermissionData(userToUpdate);
            return View(userToUpdate);
        }

        private void UpdateUserPermissions(string[] selectedPermissions, User userToUpdate)
        {
            if (selectedPermissions == null)
            {
                userToUpdate.UserPermissions = new List<UserPermission>();
                return;
            }

            var selectedPermissionsHS = new HashSet<string>(selectedPermissions);
            var instructorCourses = new HashSet<int>
                (userToUpdate.UserPermissions.Select(p => p.Permission.PermissionID));
            foreach (var perm in _context.Permissions)
            {
                if (selectedPermissionsHS.Contains(perm.PermissionID.ToString()))
                {
                    if (!instructorCourses.Contains(perm.PermissionID))
                    {
                        userToUpdate.UserPermissions.Add(new UserPermission { UserID = userToUpdate.ID, PermissionID = perm.PermissionID });
                    }
                }
                else
                {

                    if (instructorCourses.Contains(perm.PermissionID))
                    {
                        UserPermission permissionToRemove = userToUpdate.UserPermissions.SingleOrDefault(u => u.PermissionID == perm.PermissionID);
                        _context.Remove(permissionToRemove);
                    }
                }
            }
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