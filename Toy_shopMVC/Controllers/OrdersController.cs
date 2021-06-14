using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Toy_shopMVC.Models;

namespace Toy_shopMVC.Controllers
{
    public class OrdersController : Controller
    {
        private readonly Toy_shopContext _context;

        public OrdersController(Toy_shopContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var toy_shopContext = _context.Orders.Include(o => o.CodeProductInBasketNavigation)
                .ThenInclude(x=>x.CodeOfProductNavigation).Include(o => o.CodeStatusNavigation);
            return View(await toy_shopContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.CodeProductInBasketNavigation)
                .Include(o => o.CodeStatusNavigation)
                .FirstOrDefaultAsync(m => m.CodeOrder == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create(long? id)
        {
            ViewData["CodeProductInBasket"] = new SelectList(_context.ProductInBaskets, "CodeProductInBasket", "CodeProductInBasket");
            ViewData["CodeStatus"] = new SelectList(_context.Statuses, "CodeOfStatus", "Name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodeOrder,CodeProductInBasket,Sum,DateOfCreation,CodeStatus,Name,Phone,Comment,Email")] Order order, long id)
        {
            if (ModelState.IsValid)
            {
                order.CodeProductInBasket = id;
                order.Sum = (_context.ProductInBaskets.Where(x => x.CodeProductInBasket == id)
                    .Select(x => x.CodeOfProductNavigation.Price).FirstOrDefault()) *
                    (_context.ProductInBaskets.Where(x => x.CodeProductInBasket == id)
                    .Select(x => x.Quantity).FirstOrDefault());
                order.CodeStatus = 0;
                order.DateOfCreation = DateTime.Now;
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodeProductInBasket"] = new SelectList(_context.ProductInBaskets, "CodeProductInBasket", "CodeProductInBasket", order.CodeProductInBasket);
            ViewData["CodeStatus"] = new SelectList(_context.Statuses, "CodeOfStatus", "Name", order.CodeStatus);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CodeProductInBasket"] = new SelectList(_context.ProductInBaskets, "CodeProductInBasket", "CodeProductInBasket", order.CodeProductInBasket);
            ViewData["CodeStatus"] = new SelectList(_context.Statuses, "CodeOfStatus", "Name", order.CodeStatus);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("CodeOrder,CodeProductInBasket,Sum,DateOfCreation,CodeStatus,Name,Phone,Comment,Email")] Order order)
        {
            if (id != order.CodeOrder)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.CodeOrder))
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
            ViewData["CodeProductInBasket"] = new SelectList(_context.ProductInBaskets, "CodeProductInBasket", "CodeProductInBasket", order.CodeProductInBasket);
            ViewData["CodeStatus"] = new SelectList(_context.Statuses, "CodeOfStatus", "Name", order.CodeStatus);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.CodeProductInBasketNavigation)
                .Include(o => o.CodeStatusNavigation)
                .FirstOrDefaultAsync(m => m.CodeOrder == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(long id)
        {
            return _context.Orders.Any(e => e.CodeOrder == id);
        }
    }
}
