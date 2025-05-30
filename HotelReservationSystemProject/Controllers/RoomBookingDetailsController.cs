using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelReservationSystemProject.Data;
using HotelReservationSystemProject.Models;

namespace HotelReservationSystemProject.Controllers
{
    public class RoomBookingDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomBookingDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RoomBookingDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RoomBookingDetails.Include(r => r.Room).Include(r => r.RoomBooking);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RoomBookingDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomBookingDetails = await _context.RoomBookingDetails
                .Include(r => r.Room)
                .Include(r => r.RoomBooking)
                .FirstOrDefaultAsync(m => m.RoomBookingDetailsId == id);
            if (roomBookingDetails == null)
            {
                return NotFound();
            }

            return View(roomBookingDetails);
        }

        // GET: RoomBookingDetails/Create
        public IActionResult Create()
        {
            ViewData["RoomId"] = new SelectList(_context.Room, "RoomId", "RoomId");
            ViewData["RoomBookingId"] = new SelectList(_context.RoomBooking, "RoomBookingId", "RoomBookingId");
            return View();
        }

        // POST: RoomBookingDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomBookingDetailsId,ServiceDescription,RoomPrice,RoomBookingId,RoomId")] RoomBookingDetails roomBookingDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roomBookingDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(_context.Room, "RoomId", "RoomId", roomBookingDetails.RoomId);
            ViewData["RoomBookingId"] = new SelectList(_context.RoomBooking, "RoomBookingId", "RoomBookingId", roomBookingDetails.RoomBookingId);
            return View(roomBookingDetails);
        }

        // GET: RoomBookingDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomBookingDetails = await _context.RoomBookingDetails.FindAsync(id);
            if (roomBookingDetails == null)
            {
                return NotFound();
            }
            ViewData["RoomId"] = new SelectList(_context.Room, "RoomId", "RoomId", roomBookingDetails.RoomId);
            ViewData["RoomBookingId"] = new SelectList(_context.RoomBooking, "RoomBookingId", "RoomBookingId", roomBookingDetails.RoomBookingId);
            return View(roomBookingDetails);
        }

        // POST: RoomBookingDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomBookingDetailsId,ServiceDescription,RoomPrice,RoomBookingId,RoomId")] RoomBookingDetails roomBookingDetails)
        {
            if (id != roomBookingDetails.RoomBookingDetailsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomBookingDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomBookingDetailsExists(roomBookingDetails.RoomBookingDetailsId))
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
            ViewData["RoomId"] = new SelectList(_context.Room, "RoomId", "RoomId", roomBookingDetails.RoomId);
            ViewData["RoomBookingId"] = new SelectList(_context.RoomBooking, "RoomBookingId", "RoomBookingId", roomBookingDetails.RoomBookingId);
            return View(roomBookingDetails);
        }

        // GET: RoomBookingDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomBookingDetails = await _context.RoomBookingDetails
                .Include(r => r.Room)
                .Include(r => r.RoomBooking)
                .FirstOrDefaultAsync(m => m.RoomBookingDetailsId == id);
            if (roomBookingDetails == null)
            {
                return NotFound();
            }

            return View(roomBookingDetails);
        }

        // POST: RoomBookingDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roomBookingDetails = await _context.RoomBookingDetails.FindAsync(id);
            if (roomBookingDetails != null)
            {
                _context.RoomBookingDetails.Remove(roomBookingDetails);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomBookingDetailsExists(int id)
        {
            return _context.RoomBookingDetails.Any(e => e.RoomBookingDetailsId == id);
        }
    }
}
