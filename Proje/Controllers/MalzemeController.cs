using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Proje.Data;
using Proje.Models;

namespace Proje.Controllers
{
    public class MalzemeController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IStringLocalizer<MalzemeController> _localizer;

        public MalzemeController(ApplicationDbContext context, IStringLocalizer<MalzemeController> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        // GET: Malzeme
        public async Task<IActionResult> Index()
        {
            return View(await _context.Malzeme.ToListAsync());
        }

        // GET: Malzeme/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var malzeme = await _context.Malzeme
                .FirstOrDefaultAsync(m => m.MalzemeId == id);
            if (malzeme == null)
            {
                return NotFound();
            }

            return View(malzeme);
        }

        // GET: Malzeme/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Malzeme/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MalzemeId,MalzemeAdi")] Malzeme malzeme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(malzeme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(malzeme);
        }

        // GET: Malzeme/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var malzeme = await _context.Malzeme.FindAsync(id);
            if (malzeme == null)
            {
                return NotFound();
            }
            return View(malzeme);
        }

        // POST: Malzeme/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MalzemeId,MalzemeAdi")] Malzeme malzeme)
        {
            if (id != malzeme.MalzemeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(malzeme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MalzemeExists(malzeme.MalzemeId))
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
            return View(malzeme);
        }

        // GET: Malzeme/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var malzeme = await _context.Malzeme
                .FirstOrDefaultAsync(m => m.MalzemeId == id);
            if (malzeme == null)
            {
                return NotFound();
            }

            return View(malzeme);
        }

        // POST: Malzeme/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var malzeme = await _context.Malzeme.FindAsync(id);
            _context.Malzeme.Remove(malzeme);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MalzemeExists(int id)
        {
            return _context.Malzeme.Any(e => e.MalzemeId == id);
        }
    }
}
