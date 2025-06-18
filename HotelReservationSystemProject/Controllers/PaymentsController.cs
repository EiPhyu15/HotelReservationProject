using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelReservationSystemProject.Data;
using HotelReservationSystemProject.Models;
using Stripe;
using Stripe.Checkout;
using Microsoft.Extensions.Options;
using Stripe.V2;
using Stripe.FinancialConnections;

namespace HotelReservationSystemProject.Controllers
{
   
    public class PaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly StripeSettings _stripeSettings;
        private readonly IConfiguration _configuration;
        public PaymentsController(ApplicationDbContext context,IOptions<StripeSettings>stripeSettings, IConfiguration configuration )
        {
            _context = context;
            _stripeSettings = stripeSettings.Value;
            _configuration = configuration;
        }
       
        public async Task<IActionResult> Success()
        {

            return View();
        }
        public async Task<IActionResult> Cancel()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            ViewBag.StripePublishableKey = _configuration["Stripe: PublishableKey"];
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> CreateCheckOutSession()
        {

            var username = User.Identity.Name;
            var cartItems = _context.RoomItems.Where(ci => ci.CartId == username).ToList();
            var TAmount = 0.0;
            foreach (var item in cartItems)
            {
                var total = item.Price * item.Quantity;
                TAmount = TAmount + total;
            }
            var SuccessUrl = $"{Request.Scheme}://{Request.Host}/Payments/Success?session_id={{CHECKOUT_SESSION_ID}}";
            var CancelUrl= $"{Request.Scheme}: // {Request.Host}/Payments/Cancel";
            // Create a Stripe Checkout Session
            // var options = new SessionCreateOptions
            var options = new Stripe.Checkout.SessionCreateOptions
            {

                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                            Currency = "sgd",
                            UnitAmount = (long?)TAmount*100,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Total Amount",
                                Description ="Amount to charge in the Credit Card"
                            }

                        },
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                //SuccessUrl = Url.Action("Success", "Payments", null, Request.Scheme),
                //CancelUrl = Url.Action("Cancel", "Payments", null, Request.Scheme),
                SuccessUrl = SuccessUrl,
                CancelUrl = CancelUrl,
            };

            foreach (var pro in cartItems)
            {
                _context.RoomItems.Remove(pro);
            }

            await _context.SaveChangesAsync();

            var service = new Stripe.Checkout.SessionService();
            var session = await service.CreateAsync(options);
            // Redirect to Stripe SuccessUrl or CancelUrl
            return Redirect(session.Url);
        }




        // GET: Payments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Payment.Include(p => p.Receptionist).Include(p => p.RoomBooking);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .Include(p => p.Receptionist)
                .Include(p => p.RoomBooking)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            ViewData["ReceptionistId"] = new SelectList(_context.Set<Receptionist>(), "ReceptionistId", "ReceptionistId");
            ViewData["RoomBookingId"] = new SelectList(_context.Set<RoomBooking>(), "RoomBookingId", "RoomBookingId");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentId,PaymentDate,PaymentAmount,paymentType,ReceptionistId,RoomBookingId")] Payment payment)
        {
           // if (ModelState.IsValid)
           // {
                

                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            ViewData["ReceptionistId"] = new SelectList(_context.Set<Receptionist>(), "ReceptionistId", "ReceptionistId", payment.ReceptionistId);
            ViewData["RoomBookingId"] = new SelectList(_context.Set<RoomBooking>(), "RoomBookingId", "RoomBookingId", payment.RoomBookingId);
            return View(payment);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["ReceptionistId"] = new SelectList(_context.Set<Receptionist>(), "ReceptionistId", "ReceptionistId", payment.ReceptionistId);
            ViewData["RoomBookingId"] = new SelectList(_context.Set<RoomBooking>(), "RoomBookingId", "RoomBookingId", payment.RoomBookingId);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentId,PaymentDate,PaymentAmount,paymentType,ReceptionistId,RoomBookingId")] Payment payment)
        {
            if (id != payment.PaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.PaymentId))
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
            ViewData["ReceptionistId"] = new SelectList(_context.Set<Receptionist>(), "ReceptionistId", "ReceptionistId", payment.ReceptionistId);
            ViewData["RoomBookingId"] = new SelectList(_context.Set<RoomBooking>(), "RoomBookingId", "RoomBookingId", payment.RoomBookingId);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .Include(p => p.Receptionist)
                .Include(p => p.RoomBooking)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payment.FindAsync(id);
            if (payment != null)
            {
                _context.Payment.Remove(payment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payment.Any(e => e.PaymentId == id);
        }
    }
}
