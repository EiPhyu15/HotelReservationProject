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

namespace HotelReservationSystemProject.Controllers
{
    public class RoomItemsController : Controller

    {
        public string RoomCartId { get; set; }
        public const string CartSessionKey = "CartId";
        private readonly ApplicationDbContext _context;
        

        public RoomItemsController(ApplicationDbContext context)
        {
            this.RoomCartId = "";
            _context = context;
            _context = context;
        }
        
        public async Task<IActionResult> AddToCart(int id)
        {
            RoomCartId = GetCartId();
            RoomItems cartItem = new RoomItems();
            cartItem = _context.RoomItems.SingleOrDefault(c => c.CartId == RoomCartId && c.RoomId == id);
            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists.
                cartItem = new RoomItems
                {
                    RoomId = id,
                    //RoomItemsId = id,
                    Price = (int)_context.Room.SingleOrDefault(p => p.RoomId == id).Price,
                    CartId = RoomCartId,
                    //Room = _context.Room.SingleOrDefault(p => p.RoomId == id),
                    Quantity = 1,
                    //DateCreated = DateOnly.MaxValue
                };
                _context.RoomItems.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart,
                // then add one to the quantity.
                cartItem.Quantity++;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("DisplayRoomItems");
        }
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            var cartItemDelete = _context.RoomItems.Find(id);

            if (cartItemDelete != null)
            {
                _context.RoomItems.Remove(cartItemDelete);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(DisplayRoomItems));
        }

        public string GetCartId()
        {
            var session = HttpContext.Session.GetString(CartSessionKey);
            if (session == null)
            {
                if (!string.IsNullOrWhiteSpace(User.Identity.Name))
                {
                    session = User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class.
                    Guid tempCartId = Guid.NewGuid();
                    session = tempCartId.ToString();
                }
            }
            return session.ToString();
        }
        

        public List<RoomItems> GetRoomItems()
        {
            RoomCartId = GetCartId();
            return _context.RoomItems.Where(
            c => c.CartId == RoomCartId).ToList();
        }
        public IActionResult DisplayRoomItems()
        {
            var cartitems = GetRoomItems();
            ViewBag.count = cartitems.Count;
            return View(cartitems);
        }
        public async Task<IActionResult> CheckOut()
        {
            var username = User.Identity.Name;
            var roomItems = _context.RoomItems.Where(ci => ci.CartId == username).ToList();
            double TAmount = 0.0;

            foreach (var item in roomItems)
            {
                var total = item.Price * item.Quantity;
                TAmount = TAmount + total;
            }

            var paymentStatus = "Paid";
            var guestId = _context.Guest.Where(c => c.Email == username).FirstOrDefault().GuestId;
            RoomBooking roomBooking = new RoomBooking();
            //var P= new Payment();

            // P.PaymentDate = DateOnly.FromDateTime(DateTime.Now);
            // P.paymentType = "Card";
            // P.PaymentAmount = TAmount;
            // P.ReceptionistId = 1;
            // var roombookingid = ;
            // var rbId= _context.RoomBooking.Where(rb
            //_context.Payment.Add(P);
            //P.RoomBookingId = _context.RoomBooking.Where(rb => rb.RoomBookingId = roomBooking).ToList();
            // inv.InvoiceDate = DateOnly.FromDateTime(DateTime.Now);
            //inv.TotalAmount = TAmount;
            //inv.PaymentStatus = paymentStatus;
            //inv.RoomBookingId = guest;
            // _context.Invoice.Add(inv);

            await _context.SaveChangesAsync();

            return Redirect("/Payments/CheckOut");


            
        }
        public async Task<IActionResult> Index()
        {
          var applicationDbContext = _context.RoomItems.Include(q => q.Room).Include(q => q.Room);
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

