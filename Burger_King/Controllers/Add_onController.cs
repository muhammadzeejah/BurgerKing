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
    public class Add_onController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Add_onController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Add_on
        public async Task<IActionResult> Index()
        {
            return View(await _context.Add_on.ToListAsync());
        }

        // GET: Add_on/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var add_on = await _context.Add_on
                .FirstOrDefaultAsync(m => m.addon_id == id);
            if (add_on == null)
            {
                return NotFound();
            }

            return View(add_on);
        }

        // GET: Add_on/Create
        [Authorize]

        public IActionResult Create()
        {
            return View();
        }

        // POST: Add_on/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        public async Task<IActionResult> Create([Bind("addon_id,addon_name")] Add_on add_on)
        {
            if (ModelState.IsValid)
            {
                _context.Add(add_on);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(add_on);
        }

        // GET: Add_on/Edit/5
        [Authorize]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var add_on = await _context.Add_on.FindAsync(id);
            if (add_on == null)
            {
                return NotFound();
            }
            return View(add_on);
        }

        // POST: Add_on/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        public async Task<IActionResult> Edit(int id, [Bind("addon_id,addon_name")] Add_on add_on)
        {
            if (id != add_on.addon_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(add_on);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Add_onExists(add_on.addon_id))
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
            return View(add_on);
        }

        // GET: Add_on/Delete/5
        [Authorize]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var add_on = await _context.Add_on
                .FirstOrDefaultAsync(m => m.addon_id == id);
            if (add_on == null)
            {
                return NotFound();
            }

            return View(add_on);
        }

        // POST: Add_on/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var add_on = await _context.Add_on.FindAsync(id);
            _context.Add_on.Remove(add_on);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Add_onExists(int id)
        {
            return _context.Add_on.Any(e => e.addon_id == id);
        }
    }
}
