using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Burger_King.Data;
using Burger_King.Models;
using Microsoft.AspNetCore.Authorization;

namespace Burger_King.Controllers
{
    public class SaucesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SaucesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sauces
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sauces.ToListAsync());
        }

        // GET: Sauces/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sauces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        public async Task<IActionResult> Create([Bind("sauce_id,sauce_name")] Sauces sauces)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sauces);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sauces);
        }

        // GET: Sauces/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sauces = await _context.Sauces.FindAsync(id);
            if (sauces == null)
            {
                return NotFound();
            }
            return View(sauces);
        }

        // POST: Sauces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        public async Task<IActionResult> Edit(int id, [Bind("sauce_id,sauce_name")] Sauces sauces)
        {
            if (id != sauces.sauce_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sauces);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaucesExists(sauces.sauce_id))
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
            return View(sauces);
        }

        // GET: Sauces/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sauces = await _context.Sauces
                .FirstOrDefaultAsync(m => m.sauce_id == id);
            if (sauces == null)
            {
                return NotFound();
            }

            return View(sauces);
        }

        // POST: Sauces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
    
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sauces = await _context.Sauces.FindAsync(id);
            _context.Sauces.Remove(sauces);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaucesExists(int id)
        {
            return _context.Sauces.Any(e => e.sauce_id == id);
        }
    }
}
