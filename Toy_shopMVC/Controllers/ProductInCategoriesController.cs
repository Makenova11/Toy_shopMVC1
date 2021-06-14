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
    public class ProductInCategoriesController : Controller
    {
        private readonly Toy_shopContext _context;

        public ProductInCategoriesController(Toy_shopContext context)
        {
            _context = context;
        }

        // GET: ProductInCategories/Create
        public IActionResult ToBasket(long? id)
        {
            //ViewData["CodeOfCategory"] = new SelectList(_context.Categories, "CodeOfCategory", "Name");
            ViewData["CodeOfProduct"] = new SelectList(_context.Products, "CodeOfProduct", "Name");
            return View();
        }

        // POST: ProductInCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToBasket([Bind("Quantity")] ProductInBasket productInCategory, long id )
        {
            if (ModelState.IsValid)
            {
                productInCategory.CodeOfProduct = id;
                productInCategory.Quantity = 1;
                _context.Add(productInCategory);
                await _context.SaveChangesAsync();

                // Проверка на повторяющиеся элементы в корзине
                var query = _context.ProductInBaskets.GroupBy(x => x.CodeOfProduct)
              .Where(g => g.Count() > 1)
              .Select(y => new { Element = y.Key, Counter = y.Count() })
              .ToList();
                
                for(int i=0;i<query.Count;i++)
                {
                    decimal sum1 = 0;
                    var delete = await _context.ProductInBaskets.Where(x=>x.CodeOfProduct == query[i].Element).FirstAsync();
                    _context.ProductInBaskets.Remove((ProductInBasket)delete);
                    productInCategory.CodeOfProduct = query[i].Element;
                    productInCategory.Quantity = query[i].Counter;
                    var querySum = _context.ProductInBaskets.GroupBy(x => x.CodeOfProduct)
                        .Select(y => new { Prod = y.Key, Counter = y.Count() })
                        .ToList();
                    var list = querySum; // sum 6 
                    foreach(var j in querySum)
                    {
                        decimal price = _context.Products.Where(x=>x.CodeOfProduct==j.Prod).Select(x=>x.Price).FirstOrDefault();
                        sum1 += price * j.Counter;
                    }
                    ViewBag.Sum = sum1;
                    ViewData["Sum"] = sum1;
                    TempData["Sum"] = sum1;
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            
            //ViewData["CodeOfProduct"] = new SelectList(_context.Products, "CodeOfProduct", "Name", productInCategory.CodeOfProduct);
            return View(productInCategory);
        }

        // GET: ProductInCategories
        public async Task<IActionResult> Index()
        {
            var toy_shopContext = _context.ProductInCategories.Include(p => p.CodeOfCategoryNavigation).Include(p => p.CodeOfProductNavigation);
            return View(await toy_shopContext.ToListAsync());
        }

        // GET: ProductInCategories/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInCategory = await _context.ProductInCategories
                .Include(p => p.CodeOfCategoryNavigation)
                .Include(p => p.CodeOfProductNavigation)
                .FirstOrDefaultAsync(m => m.CodeOfProduct == id);
            if (productInCategory == null)
            {
                return NotFound();
            }

            return View(productInCategory);
        }

        // GET: ProductInCategories/Create
        public IActionResult Create()
        {
            ViewData["CodeOfCategory"] = new SelectList(_context.Categories, "CodeOfCategory", "Name");
            ViewData["CodeOfProduct"] = new SelectList(_context.Products, "CodeOfProduct", "Name");
            return View();
        }

        // POST: ProductInCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodeOfProduct,CodeOfCategory")] ProductInCategory productInCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productInCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodeOfCategory"] = new SelectList(_context.Categories, "CodeOfCategory", "Name", productInCategory.CodeOfCategory);
            ViewData["CodeOfProduct"] = new SelectList(_context.Products, "CodeOfProduct", "Name", productInCategory.CodeOfProduct);
            return View(productInCategory);
        }

        // GET: ProductInCategories/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInCategory = await _context.ProductInCategories.FindAsync(id);
            if (productInCategory == null)
            {
                return NotFound();
            }
            ViewData["CodeOfCategory"] = new SelectList(_context.Categories, "CodeOfCategory", "Name", productInCategory.CodeOfCategory);
            ViewData["CodeOfProduct"] = new SelectList(_context.Products, "CodeOfProduct", "Name", productInCategory.CodeOfProduct);
            return View(productInCategory);
        }

        // POST: ProductInCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("CodeOfProduct,CodeOfCategory")] ProductInCategory productInCategory)
        {
            if (id != productInCategory.CodeOfProduct)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productInCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductInCategoryExists(productInCategory.CodeOfProduct))
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
            ViewData["CodeOfCategory"] = new SelectList(_context.Categories, "CodeOfCategory", "Name", productInCategory.CodeOfCategory);
            ViewData["CodeOfProduct"] = new SelectList(_context.Products, "CodeOfProduct", "Name", productInCategory.CodeOfProduct);
            return View(productInCategory);
        }

        // GET: ProductInCategories/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInCategory = await _context.ProductInCategories
                .Include(p => p.CodeOfCategoryNavigation)
                .Include(p => p.CodeOfProductNavigation)
                .FirstOrDefaultAsync(m => m.CodeOfProduct == id);
            if (productInCategory == null)
            {
                return NotFound();
            }

            return View(productInCategory);
        }

        // POST: ProductInCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var productInCategory = await _context.ProductInCategories.FindAsync(id);
            _context.ProductInCategories.Remove(productInCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductInCategoryExists(long id)
        {
            return _context.ProductInCategories.Any(e => e.CodeOfProduct == id);
        }
    }
}
