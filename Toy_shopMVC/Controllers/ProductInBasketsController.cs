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
    public class ProductInBasketsController : Controller
    {
        private readonly Toy_shopContext _context;

        public ProductInBasketsController(Toy_shopContext context)
        {
            _context = context;
        }

        // GET: ProductInBaskets
        public async Task<IActionResult> Index()
        {
            var toy_shopContext = _context.ProductInBaskets.Include(p => p.CodeOfProductNavigation);
            return View(await toy_shopContext.ToListAsync());
        }

        //// GET: ProductInBaskets
        //public async Task<IActionResult> Sum()
        //{
        //    decimal sum = 0;
        //    foreach (var i in _context.ProductInBaskets)
        //    {
        //        sum += i.Quantity * i.CodeOfProductNavigation.Price;
        //    }
        //    ViewBag.Sum = sum;
        //    return View();
        //}
        // GET: ProductInBaskets/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInBasket = await _context.ProductInBaskets
                .Include(p => p.CodeOfProductNavigation)
                .FirstOrDefaultAsync(m => m.CodeProductInBasket == id);
            if (productInBasket == null)
            {
                return NotFound();
            }

            return View(productInBasket);
        }

        // GET: ProductInBaskets/Create
        public IActionResult Create()
        {
            ViewData["CodeOfProduct"] = new SelectList(_context.Products, "CodeOfProduct", "Name");
            return View();
        }

        // POST: ProductInBaskets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodeProductInBasket,CodeOfProduct,Quantity")] ProductInBasket productInBasket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productInBasket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodeOfProduct"] = new SelectList(_context.Products, "CodeOfProduct", "Name", productInBasket.CodeOfProduct);
            return View(productInBasket);
        }

        // GET: ProductInBaskets/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInBasket = await _context.ProductInBaskets.FindAsync(id);
            if (productInBasket == null)
            {
                return NotFound();
            }
            ViewData["CodeOfProduct"] = new SelectList(_context.Products, "CodeOfProduct", "Name", productInBasket.CodeOfProduct);
            return View(productInBasket);
        }

        // POST: ProductInBaskets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("CodeProductInBasket,CodeOfProduct,Quantity")] ProductInBasket productInBasket)
        {
            if (id != productInBasket.CodeProductInBasket)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productInBasket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductInBasketExists(productInBasket.CodeProductInBasket))
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
            ViewData["CodeOfProduct"] = new SelectList(_context.Products, "CodeOfProduct", "Name", productInBasket.CodeOfProduct);
            return View(productInBasket);
        }

        // GET: ProductInBaskets/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInBasket = await _context.ProductInBaskets
                .Include(p => p.CodeOfProductNavigation)
                .FirstOrDefaultAsync(m => m.CodeProductInBasket == id);
            if (productInBasket == null)
            {
                return NotFound();
            }

            return View(productInBasket);
        }

        // POST: ProductInBaskets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var productInBasket = await _context.ProductInBaskets.FindAsync(id);
            _context.ProductInBaskets.Remove(productInBasket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductInBasketExists(long id)
        {
            return _context.ProductInBaskets.Any(e => e.CodeProductInBasket == id);
        }
    }
}
