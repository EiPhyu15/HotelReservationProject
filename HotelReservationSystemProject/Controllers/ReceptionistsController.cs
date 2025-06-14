﻿using System;
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
    public class ReceptionistsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReceptionistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Receptionists
        public async Task<IActionResult> Index()
        {
            return View(await _context.Receptionist.ToListAsync());
        }

        // GET: Receptionists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receptionist = await _context.Receptionist
                .FirstOrDefaultAsync(m => m.ReceptionistId == id);
            if (receptionist == null)
            {
                return NotFound();
            }

            return View(receptionist);
        }

        // GET: Receptionists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Receptionists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReceptionistId,ReceptionistFName,ReceptionistLName,ReceptionistInfo")] Receptionist receptionist)
        {
            // if (ModelState.IsValid)
            // {
           
            _context.Add(receptionist);
                await _context.SaveChangesAsync();
            return Redirect("/Identity/Account/Login");
            //}
            return View(receptionist);
        }

        // GET: Receptionists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receptionist = await _context.Receptionist.FindAsync(id);
            if (receptionist == null)
            {
                return NotFound();
            }
            return View(receptionist);
        }

        // POST: Receptionists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReceptionistId,ReceptionistFName,ReceptionistLName,ReceptionistInfo")] Receptionist receptionist)
        {
            if (id != receptionist.ReceptionistId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receptionist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceptionistExists(receptionist.ReceptionistId))
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
            return View(receptionist);
        }

        // GET: Receptionists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receptionist = await _context.Receptionist
                .FirstOrDefaultAsync(m => m.ReceptionistId == id);
            if (receptionist == null)
            {
                return NotFound();
            }

            return View(receptionist);
        }

        // POST: Receptionists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var receptionist = await _context.Receptionist.FindAsync(id);
            if (receptionist != null)
            {
                _context.Receptionist.Remove(receptionist);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceptionistExists(int id)
        {
            return _context.Receptionist.Any(e => e.ReceptionistId == id);
        }
    }
}
