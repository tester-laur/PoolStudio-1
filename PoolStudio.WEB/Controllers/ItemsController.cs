using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PoolStudio.WEB.Data;
using PoolStudio.WEB.Models;

namespace PoolStudio.WEB.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        [Obsolete]
        private readonly IHostingEnvironment _hostEnvironment;

        [Obsolete]
        public ItemsController(ApplicationDbContext context, IHostingEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Category()
        {
            return View(await _context.Item.ToListAsync());
        }


        // GET: Items
        public async Task<IActionResult> Index(int pg = 1)
        {
            List<Item> items = _context.Item.Include(i => i.Clasification).ToList();

            const int pageSize = 5;
            if (pg < 1)
                pg = 1;

            int recsCount = items.Count();

            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = items.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string varSearch)
        {
            ViewData["GetDetails"] = varSearch;

            var varQuery = from x in _context.Item.Include(o => o.Clasification) select x;
            if (!String.IsNullOrEmpty(varSearch))
            {
                varQuery = varQuery.Where(
                    x => x.Clasification.ItemType.Contains(varSearch) || 
                    x.ItemName.Contains(varSearch) || 
                    x.Modelo.Contains(varSearch) || 
                    x.Brand.Contains(varSearch) || 
                    x.Comment.Contains(varSearch)
                    );
            }

            return View(await varQuery.AsNoTracking().ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> CategorySearch(string varCatSearch)
        {
            ViewData["GetDetails"] = varCatSearch;

            var varQuery = from x in _context.Item.Include(o => o.Clasification) select x;
            if (!String.IsNullOrEmpty(varCatSearch))
            {
                varQuery = varQuery.Where(
                    x => x.Clasification.ItemType.Contains(varCatSearch));
            }

            return View(await varQuery.AsNoTracking().ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .Include(i => i.Clasification)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["ClasificationId"] = new SelectList(_context.Clasification, "ClasificationId", "ItemType");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<IActionResult> Create([Bind("ItemId,ImageFile,ClasificationId,ItemName,Modelo,Brand,Comment")] Item item)
        {
            if (ModelState.IsValid)
            {
                // Save image to wwwroot/UserImages
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(item.ImageFile.FileName);
                string extension = Path.GetExtension(item.ImageFile.FileName);
                item.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Imagenes/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await item.ImageFile.CopyToAsync(fileStream);
                }

                // Insert record
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClasificationId"] = new SelectList(_context.Clasification, "ClasificationId", "ItemType", item.ClasificationId);
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["ClasificationId"] = new SelectList(_context.Clasification, "ClasificationId", "ItemType", item.ClasificationId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,ImageName,ClasificationId,ItemName,Modelo,Brand,Comment")] Item item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId))
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
            ViewData["ClasificationId"] = new SelectList(_context.Clasification, "ClasificationId", "ItemType", item.ClasificationId);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .Include(i => i.Clasification)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Item.FindAsync(id);

            // Delete image from wwwroot/UserImages
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "Imagenes", item.ImageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);

            // Delete the record
            _context.Item.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.ItemId == id);
        }
    }
}
