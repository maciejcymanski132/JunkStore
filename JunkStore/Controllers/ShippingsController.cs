using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JunkStore.Data;
using JunkStore.Models;
using Microsoft.AspNetCore.Authorization;

namespace JunkStore.Controllers
{
    [Authorize]
    public class ShippingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShippingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Shippings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Shipping.Include(s => s.Order);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Shippings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Shipping == null)
            {
                return NotFound();
            }

            var shipping = await _context.Shipping
                .Include(s => s.Order)
                .FirstOrDefaultAsync(m => m.ShippingId == id);
            if (shipping == null)
            {
                return NotFound();
            }

            return View(shipping);
        }

        [Authorize(Roles = "Admin")]
        // GET: Shippings/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Order, "OrderId", "OrderId");
            return View();
        }
        [Authorize(Roles = "Admin")]

        // POST: Shippings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShippingId,OrderId,ShippingMethod,TrackingNumber,ShippingDate,Status")] Shipping shipping)
        {
            if (ModelState.IsValid)
            {
                shipping.ShippingId = Guid.NewGuid();
                _context.Add(shipping);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Order, "OrderId", "OrderId", shipping.OrderId);
            return View(shipping);
        }
        [Authorize(Roles = "Admin")]

        // GET: Shippings/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Shipping == null)
            {
                return NotFound();
            }

            var shipping = await _context.Shipping.FindAsync(id);
            if (shipping == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Order, "OrderId", "OrderId", shipping.OrderId);
            return View(shipping);
        }
        [Authorize(Roles = "Admin")]

        // POST: Shippings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ShippingId,OrderId,ShippingMethod,TrackingNumber,ShippingDate,Status")] Shipping shipping)
        {
            if (id != shipping.ShippingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shipping);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShippingExists(shipping.ShippingId))
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
            ViewData["OrderId"] = new SelectList(_context.Order, "OrderId", "OrderId", shipping.OrderId);
            return View(shipping);
        }
        [Authorize(Roles = "Admin")]

        // GET: Shippings/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Shipping == null)
            {
                return NotFound();
            }

            var shipping = await _context.Shipping
                .Include(s => s.Order)
                .FirstOrDefaultAsync(m => m.ShippingId == id);
            if (shipping == null)
            {
                return NotFound();
            }

            return View(shipping);
        }
        [Authorize(Roles = "Admin")]

        // POST: Shippings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Shipping == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Shipping'  is null.");
            }
            var shipping = await _context.Shipping.FindAsync(id);
            if (shipping != null)
            {
                _context.Shipping.Remove(shipping);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShippingExists(Guid id)
        {
          return _context.Shipping.Any(e => e.ShippingId == id);
        }
    }
}
