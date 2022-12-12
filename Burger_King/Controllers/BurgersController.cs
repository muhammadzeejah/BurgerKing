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
    public class BurgersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BurgersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Burgers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Burgers.Include(b => b.burgerAddOn).Include(b => b.burgerSauce);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Burgers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var burgers = await _context.Burgers
                .Include(b => b.burgerAddOn)
                .Include(b => b.burgerSauce)
                .FirstOrDefaultAsync(m => m.burger_id == id);
            if (burgers == null)
            {
                return NotFound();
            }

            return View(burgers);
        }

        // GET: Burgers/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["addon_fid"] = new SelectList(_context.Add_on, "addon_id", "addon_name");
            ViewData["sauce_fid"] = new SelectList(_context.Sauces, "sauce_id", "sauce_name");
            return View();
        }

        // POST: Burgers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        public async Task<IActionResult> Create([Bind("burger_id,burger_name,burger_price,burger_url,sauce_fid,addon_fid")] Burgers burgers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(burgers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["addon_fid"] = new SelectList(_context.Add_on, "addon_id", "addon_name", burgers.addon_fid);
            ViewData["sauce_fid"] = new SelectList(_context.Sauces, "sauce_id", "sauce_name", burgers.sauce_fid);
            return View(burgers);
        }

        // GET: Burgers/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var burgers = await _context.Burgers.FindAsync(id);
            if (burgers == null)
            {
                return NotFound();
            }
            ViewData["addon_fid"] = new SelectList(_context.Add_on, "addon_id", "addon_name", burgers.addon_fid);
            ViewData["sauce_fid"] = new SelectList(_context.Sauces, "sauce_id", "sauce_name", burgers.sauce_fid);
            return View(burgers);
        }

        // POST: Burgers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        public async Task<IActionResult> Edit(int id, [Bind("burger_id,burger_name,burger_price,burger_url,sauce_fid,addon_fid")] Burgers burgers)
        {
            if (id != burgers.burger_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(burgers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BurgersExists(burgers.burger_id))
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
            ViewData["addon_fid"] = new SelectList(_context.Add_on, "addon_id", "addon_name", burgers.addon_fid);
            ViewData["sauce_fid"] = new SelectList(_context.Sauces, "sauce_id", "sauce_name", burgers.sauce_fid);
            return View(burgers);
        }

        // GET: Burgers/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var burgers = await _context.Burgers
                .Include(b => b.burgerAddOn)
                .Include(b => b.burgerSauce)
                .FirstOrDefaultAsync(m => m.burger_id == id);
            if (burgers == null)
            {
                return NotFound();
            }

            return View(burgers);
        }

        // POST: Burgers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var burgers = await _context.Burgers.FindAsync(id);
            _context.Burgers.Remove(burgers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BurgersExists(int id)
        {
            return _context.Burgers.Any(e => e.burger_id == id);
        }
    }
}
