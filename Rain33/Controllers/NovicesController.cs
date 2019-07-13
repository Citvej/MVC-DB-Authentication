using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rain33.Models;

namespace Rain33.Controllers
{
    public class NovicesController : Controller
    {
        private readonly NoviceContext _context;

        public NovicesController(NoviceContext context)
        {
            _context = context;
        }

        // GET: Novices
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Index(string searchString)
        {
            var noviceContext = _context.Novica.Include(n => n.Uporabniki);

            var novice = from n in _context.Novica
                         select n;

            if (!String.IsNullOrEmpty(searchString))
            {
                novice = novice.Where(s => s.besedilo.Contains(searchString));
            }

            return View(await novice.ToListAsync());
            
        }

        // GET: Novices/Details/5
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var novice = await _context.Novica
                .Include(n => n.Uporabniki)
                .FirstOrDefaultAsync(m => m.NoviceId == id);
            if (novice == null)
            {
                return NotFound();
            }

            return View(novice);
        }

        // GET: Novices/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["UporabnikiId"] = new SelectList(_context.Uporabnik, "UporabnikiId", "UporabnikiId");
            return View();
        }

        // POST: Novices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("NoviceId,datum,avtor,besedilo,UporabnikiId")] Novice novice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(novice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UporabnikiId"] = new SelectList(_context.Uporabnik, "UporabnikiId", "UporabnikiId", novice.UporabnikiId);
            return View(novice);
        }

        // GET: Novices/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var novice = await _context.Novica.FindAsync(id);
            if (novice == null)
            {
                return NotFound();
            }
            ViewData["UporabnikiId"] = new SelectList(_context.Uporabnik, "UporabnikiId", "UporabnikiId", novice.UporabnikiId);
            return View(novice);
        }

        // POST: Novices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("NoviceId,datum,avtor,besedilo,UporabnikiId")] Novice novice)
        {
            if (id != novice.NoviceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(novice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoviceExists(novice.NoviceId))
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
            ViewData["UporabnikiId"] = new SelectList(_context.Uporabnik, "UporabnikiId", "UporabnikiId", novice.UporabnikiId);
            return View(novice);
        }

        // GET: Novices/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var novice = await _context.Novica
                .Include(n => n.Uporabniki)
                .FirstOrDefaultAsync(m => m.NoviceId == id);
            if (novice == null)
            {
                return NotFound();
            }

            return View(novice);
        }

        // POST: Novices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var novice = await _context.Novica.FindAsync(id);
            _context.Novica.Remove(novice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoviceExists(int id)
        {
            return _context.Novica.Any(e => e.NoviceId == id);
        }
    }
}
