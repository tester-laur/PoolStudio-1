using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PoolStudio.WEB.Data;
using PoolStudio.WEB.Models;

namespace PoolStudio.WEB.Controllers
{
    public class ItemTestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemTestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ItemTests
        public async Task<IActionResult> Index(int pg = 1)
        {
            List<ItemTest> itemTests = _context.ItemTests.ToList();

            const int pageSize = 5;
            if (pg < 1)
                pg = 1;

            int recsCount = itemTests.Count();

            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = itemTests.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            return View(data);
        }

        // GET: ItemTests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemTest = await _context.ItemTests
                .FirstOrDefaultAsync(m => m.ItemTestId == id);
            if (itemTest == null)
            {
                return NotFound();
            }

            return View(itemTest);
        }

        // GET: ItemTests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ItemTests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemTestId,ItemName,Modelo,Brand,Comment")] ItemTest itemTest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemTest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(itemTest);
        }

        // GET: ItemTests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemTest = await _context.ItemTests.FindAsync(id);
            if (itemTest == null)
            {
                return NotFound();
            }
            return View(itemTest);
        }

        // POST: ItemTests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemTestId,ItemName,Modelo,Brand,Comment")] ItemTest itemTest)
        {
            if (id != itemTest.ItemTestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemTest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemTestExists(itemTest.ItemTestId))
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
            return View(itemTest);
        }

        // GET: ItemTests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemTest = await _context.ItemTests
                .FirstOrDefaultAsync(m => m.ItemTestId == id);
            if (itemTest == null)
            {
                return NotFound();
            }

            return View(itemTest);
        }

        // POST: ItemTests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemTest = await _context.ItemTests.FindAsync(id);
            _context.ItemTests.Remove(itemTest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemTestExists(int id)
        {
            return _context.ItemTests.Any(e => e.ItemTestId == id);
        }
    }
}
