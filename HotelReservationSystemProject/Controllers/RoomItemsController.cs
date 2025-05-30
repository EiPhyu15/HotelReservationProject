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
    public class RoomItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RoomItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RoomItems.Include(r => r.Room);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RoomItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomItems = await _context.RoomItems
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.RoomItemsId == id);
            if (roomItems == null)
            {
                return NotFound();
            }

            return View(roomItems);
        }

        // GET: RoomItems/Create
        public IActionResult Create()
        {
            ViewData["RoomId"] = new SelectList(_context.Room, "RoomId", "RoomId");
            return View();
        }

        // POST: RoomItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomItemsId,CartId,Quantity,Price,RoomId")] RoomItems roomItems)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roomItems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(_context.Room, "RoomId", "RoomId", roomItems.RoomId);
            return View(roomItems);
        }

        // GET: RoomItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomItems = await _context.RoomItems.FindAsync(id);
            if (roomItems == null)
            {
                return NotFound();
            }
            ViewData["RoomId"] = new SelectList(_context.Room, "RoomId", "RoomId", roomItems.RoomId);
            return View(roomItems);
        }

        // POST: RoomItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomItemsId,CartId,Quantity,Price,RoomId")] RoomItems roomItems)
        {
            if (id != roomItems.RoomItemsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomItems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomItemsExists(roomItems.RoomItemsId))
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
            ViewData["RoomId"] = new SelectList(_context.Room, "RoomId", "RoomId", roomItems.RoomId);
            return View(roomItems);
        }

        // GET: RoomItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomItems = await _context.RoomItems
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.RoomItemsId == id);
            if (roomItems == null)
            {
                return NotFound();
            }

            return View(roomItems);
        }

        // POST: RoomItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roomItems = await _context.RoomItems.FindAsync(id);
            if (roomItems != null)
            {
                _context.RoomItems.Remove(roomItems);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomItemsExists(int id)
        {
            return _context.RoomItems.Any(e => e.RoomItemsId == id);
        }
    }
}
