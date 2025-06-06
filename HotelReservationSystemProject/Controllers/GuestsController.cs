using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelReservationSystemProject.Data;
using HotelReservationSystemProject.Models;
using Microsoft.AspNetCore.Identity;

namespace HotelReservationSystemProject.Controllers
 
{
    public class GuestsController : Controller

    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        

        public GuestsController(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> MyProfile()
        {
            string username = User.Identity.Name;
            var getProfile = _context.Guest.Where(c => c.Email == username).FirstOrDefault();
            return View(getProfile);
        }

        // GET: Guests
        public async Task<IActionResult> Index()
        {
            return View(await _context.Guest.ToListAsync());
        }

        // GET: Guests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guest = await _context.Guest
                .FirstOrDefaultAsync(m => m.GuestId == id);
            if (guest == null)
            {
                return NotFound();
            }

            return View(guest);
        }

        // GET: Guests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Guests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GuestId,GuestFName,GuestLName,PassportNo,Email")] Guest guest)
        {
          
             _context.Add(guest);
            var guestUserId = _context.Users.Where(u => u.Email == guest.Email).FirstOrDefault().Id;
            var guestRoleId = _context.Roles.Where(r => r.Name == "Guest").FirstOrDefault().Id;
            var roleName = _context.Roles.Where(r => r.Name == "Guest").FirstOrDefault().Name;
            var userRole = new IdentityUserRole<string>
            {
                UserId = guestUserId,
                RoleId = guestRoleId
            };
            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();
            return Redirect("/Identity/Account/Login");
            return View(guest);
            }

        

        // GET: Guests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guest = await _context.Guest.FindAsync(id);
            if (guest == null)
            {
                return NotFound();
            }
            return View(guest);
        }

        // POST: Guests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GuestId,GuestFName,GuestLName,PassportNo,Email")] Guest guest)
        {
            if (id != guest.GuestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuestExists(guest.GuestId))
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
            return View(guest);
        }

        // GET: Guests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guest = await _context.Guest
                .FirstOrDefaultAsync(m => m.GuestId == id);
            if (guest == null)
            {
                return NotFound();
            }

            return View(guest);
        }

        // POST: Guests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guest = await _context.Guest.FindAsync(id);
            if (guest != null)
            {
                _context.Guest.Remove(guest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GuestExists(int id)
        {
            return _context.Guest.Any(e => e.GuestId == id);
        }
    }
}
