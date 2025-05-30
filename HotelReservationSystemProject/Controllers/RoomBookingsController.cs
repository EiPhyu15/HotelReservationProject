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
    public class RoomBookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomBookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RoomBookings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RoomBooking.Include(r => r.Guest).Include(r => r.Receptionist);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RoomBookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomBooking = await _context.RoomBooking
                .Include(r => r.Guest)
                .Include(r => r.Receptionist)
                .FirstOrDefaultAsync(m => m.RoomBookingId == id);
            if (roomBooking == null)
            {
                return NotFound();
            }

            return View(roomBooking);
        }

        // GET: RoomBookings/Create
        public IActionResult Create()
        {
            ViewData["GuestId"] = new SelectList(_context.Guest, "GuestId", "GuestId");
            ViewData["ReceptionistId"] = new SelectList(_context.Receptionist, "ReceptionistId", "ReceptionistId");
            return View();
        }

        // POST: RoomBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomBookingId,CheckInDate,CheckOutDate,Status,GuestId,ReceptionistId")] RoomBooking roomBooking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roomBooking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GuestId"] = new SelectList(_context.Guest, "GuestId", "GuestId", roomBooking.GuestId);
            ViewData["ReceptionistId"] = new SelectList(_context.Receptionist, "ReceptionistId", "ReceptionistId", roomBooking.ReceptionistId);
            return View(roomBooking);
        }

        // GET: RoomBookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomBooking = await _context.RoomBooking.FindAsync(id);
            if (roomBooking == null)
            {
                return NotFound();
            }
            ViewData["GuestId"] = new SelectList(_context.Guest, "GuestId", "GuestId", roomBooking.GuestId);
            ViewData["ReceptionistId"] = new SelectList(_context.Receptionist, "ReceptionistId", "ReceptionistId", roomBooking.ReceptionistId);
            return View(roomBooking);
        }

        // POST: RoomBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomBookingId,CheckInDate,CheckOutDate,Status,GuestId,ReceptionistId")] RoomBooking roomBooking)
        {
            if (id != roomBooking.RoomBookingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomBooking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomBookingExists(roomBooking.RoomBookingId))
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
            ViewData["GuestId"] = new SelectList(_context.Guest, "GuestId", "GuestId", roomBooking.GuestId);
            ViewData["ReceptionistId"] = new SelectList(_context.Receptionist, "ReceptionistId", "ReceptionistId", roomBooking.ReceptionistId);
            return View(roomBooking);
        }

        // GET: RoomBookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomBooking = await _context.RoomBooking
                .Include(r => r.Guest)
                .Include(r => r.Receptionist)
                .FirstOrDefaultAsync(m => m.RoomBookingId == id);
            if (roomBooking == null)
            {
                return NotFound();
            }

            return View(roomBooking);
        }

        // POST: RoomBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roomBooking = await _context.RoomBooking.FindAsync(id);
            if (roomBooking != null)
            {
                _context.RoomBooking.Remove(roomBooking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomBookingExists(int id)
        {
            return _context.RoomBooking.Any(e => e.RoomBookingId == id);
        }
    }
}
