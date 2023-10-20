using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bug_Tracker.Data;
using Bug_Tracker.Models;
using Microsoft.AspNetCore.Authorization;

namespace Bug_Tracker.Controllers
{
    public class BugTrackersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BugTrackersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BugTrackers
        public async Task<IActionResult> Index()
        {
              return _context.BugTracker != null ? 
                          View(await _context.BugTracker.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.BugTracker'  is null.");
        }

        // GET: BugTrackers/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // Post: BugTrackers/SearchResults
        public async Task<IActionResult> SearchResults(String SearchPhrase)
        {
            return View("Index", await _context.BugTracker.Where( b => b.Bug.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: BugTrackers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BugTracker == null)
            {
                return NotFound();
            }

            var bugTracker = await _context.BugTracker
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bugTracker == null)
            {
                return NotFound();
            }

            return View(bugTracker);
        }

        // GET: BugTrackers/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: BugTrackers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Bug,Description,Priority,Status,Asignee")] BugTracker bugTracker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bugTracker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bugTracker);
        }

        // GET: BugTrackers/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BugTracker == null)
            {
                return NotFound();
            }

            var bugTracker = await _context.BugTracker.FindAsync(id);
            if (bugTracker == null)
            {
                return NotFound();
            }
            return View(bugTracker);
        }

        // POST: BugTrackers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Bug,Description,Priority,Status,Asignee")] BugTracker bugTracker)
        {
            if (id != bugTracker.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bugTracker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BugTrackerExists(bugTracker.ID))
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
            return View(bugTracker);
        }

        // GET: BugTrackers/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BugTracker == null)
            {
                return NotFound();
            }

            var bugTracker = await _context.BugTracker
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bugTracker == null)
            {
                return NotFound();
            }

            return View(bugTracker);
        }

        // POST: BugTrackers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BugTracker == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BugTracker'  is null.");
            }
            var bugTracker = await _context.BugTracker.FindAsync(id);
            if (bugTracker != null)
            {
                _context.BugTracker.Remove(bugTracker);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BugTrackerExists(int id)
        {
          return (_context.BugTracker?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
