using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JunkStore.Data;
using JunkStore.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Microsoft.AspNetCore.Authorization;

namespace JunkStore.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
              return View(await _context.Product.Include(p=> p.ProductType).ToListAsync());
        }
        
        public async Task<IActionResult> ProductsByName(string productName)
        {
            var products = from p in _context.Product
                           where p.Name.Contains(productName)
                           select p;
            return View(await products.Include(p => p.ProductType).ToListAsync());
        }

        public async Task<IActionResult> ManageProducts()
        {
            return View(await _context.Product.Include(p => p.ProductType).ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "ProductTypeId", "Name");
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,Description,Price,ProductTypeId")] Product product, IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                using (var imageStream = new MemoryStream())
                {
                    await image.CopyToAsync(imageStream);
                    using (var uploadedImage = Image.Load(imageStream.ToArray()))
                    {
                        uploadedImage.Mutate(x => x.Resize(200, 200));
                        using (var output = new MemoryStream())
                        {
                            uploadedImage.SaveAsPng(output);
                            product.Image = output.ToArray();
                        }
                    }
                }
            }

            var productType = _context.ProductType.Where(p => p.ProductTypeId.ToString() == product.ProductTypeId).First();
            product.ProductType = productType;

            _context.Add(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,Description,Price,Image,ProductTypeId")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Product'  is null.");
            }
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

            public async Task<IActionResult> CheapestProducts(int n, bool isAscending = false)
            {
                var products = (from p in _context.Product
                                orderby p.Price ascending
                                select p).Take(n);
                return View(await products.ToListAsync());
            }

        public async Task<IActionResult> ProductsByTypeAndPriceRange(Guid productTypeId, decimal minPrice, decimal maxPrice)
        {
            var products = from p in _context.Product
                           where p.ProductTypeId == productTypeId.ToString() && p.Price >= minPrice && p.Price <= maxPrice
                           select p;
            return View(await products.ToListAsync());
        }

        private bool ProductExists(int id)
        {
          return _context.Product.Any(e => e.ProductId == id);
        }
    }
}
