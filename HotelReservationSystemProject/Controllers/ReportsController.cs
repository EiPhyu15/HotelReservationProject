﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelReservationSystemProject.Data;
using HotelReservationSystemProject.Models;
using Stripe.Reporting;

namespace HotelReservationSystemProject.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult ListBooked()
        {
           
            var lstbooked = _context.RoomBooking.Where(rb => rb.Status == "Confirmed").Include(rb=>rb.Guest).Include(rb=>rb.Receptionist).ToList();
            
            return View(lstbooked);
        }
        public IActionResult ListAvaliable()
        {

            var lstavaliable = _context.RoomBooking.Where(rb => rb.Status == "Available").Include(rb => rb.Guest).Include(rb => rb.Receptionist).ToList();



            return View(lstavaliable);
        }
        // GET: Reports
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Report.Include(r => r.Manager).Include(r => r.Receptionist);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Reports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Report
                .Include(r => r.Manager)
                .Include(r => r.Receptionist)
                .FirstOrDefaultAsync(m => m.ReportId == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // GET: Reports/Create
        public IActionResult Create()
        {
            ViewData["ManagerId"] = new SelectList(_context.Manager, "ManagerId", "ManagerId");
            ViewData["ReceptionistId"] = new SelectList(_context.Receptionist, "ReceptionistId", "ReceptionistId");
            return View();
        }

        // POST: Reports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReportId,GeneratedDate,ReportTitle,ManagerId,ReceptionistId")] Report report)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(report);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            ViewData["ManagerId"] = new SelectList(_context.Manager, "ManagerId", "ManagerId", report.ManagerId);
            ViewData["ReceptionistId"] = new SelectList(_context.Receptionist, "ReceptionistId", "ReceptionistId", report.ReceptionistId);
            return View(report);
        }

        // GET: Reports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Report.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            ViewData["ManagerId"] = new SelectList(_context.Manager, "ManagerId", "ManagerId", report.ManagerId);
            ViewData["ReceptionistId"] = new SelectList(_context.Receptionist, "ReceptionistId", "ReceptionistId", report.ReceptionistId);
            return View(report);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReportId,GeneratedDate,ReportTitle,ManagerId,ReceptionistId")] Report report)
        {
            if (id != report.ReportId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(report);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportExists(report.ReportId))
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
            ViewData["ManagerId"] = new SelectList(_context.Manager, "ManagerId", "ManagerId", report.ManagerId);
            ViewData["ReceptionistId"] = new SelectList(_context.Receptionist, "ReceptionistId", "ReceptionistId", report.ReceptionistId);
            return View(report);
        }

        // GET: Reports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Report
                .Include(r => r.Manager)
                .Include(r => r.Receptionist)
                .FirstOrDefaultAsync(m => m.ReportId == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var report = await _context.Report.FindAsync(id);
            if (report != null)
            {
                _context.Report.Remove(report);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportExists(int id)
        {
            return _context.Report.Any(e => e.ReportId == id);
        }
    }
}
