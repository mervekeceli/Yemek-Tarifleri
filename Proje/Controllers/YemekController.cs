using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Proje.Data;
using Proje.Models;

namespace Proje.Controllers
{
    public class YemekController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _hostingEnvironment;

        private readonly IStringLocalizer<YemekController> _localizer;

        public YemekController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment, IStringLocalizer<YemekController> localizer)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _localizer = localizer;
        }

        // GET: Yemek
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Yemek.Include(y => y.Kategori);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Yemek/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yemek = await _context.Yemek
                .Include(y => y.Kategori)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (yemek == null)
            {
                return NotFound();
            }

            return View(yemek);
        }

        // GET: Yemek/Create
        public IActionResult Create()
        {
            ViewData["KategoriId"] = new SelectList(_context.Kategori, "KategoriId", "KategoriAdi");
            return View();
        }

        // POST: Yemek/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Adi,Tarif,Resim,KacKisilik,HazirlikSuresi,PisirmeSuresi,YuklemeTarihi,KategoriId")] Yemek yemek)
        {
            if (ModelState.IsValid)
            {
                //************
                string webRootPath = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"images\yemek");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                yemek.Resim = @"\images\yemek\" + fileName + extension;

                //****************
                _context.Add(yemek);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriId"] = new SelectList(_context.Kategori, "KategoriId", "KategoriAdi", yemek.KategoriId);
            return View(yemek);
        }

        // GET: Yemek/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yemek = await _context.Yemek.FindAsync(id);
            if (yemek == null)
            {
                return NotFound();
            }
            ViewData["KategoriId"] = new SelectList(_context.Kategori, "KategoriId", "KategoriAdi", yemek.KategoriId);
            return View(yemek);
        }

        // POST: Yemek/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Adi,Tarif,Resim,KacKisilik,HazirlikSuresi,PisirmeSuresi,YuklemeTarihi,KategoriId")] Yemek yemek)
        {
            if (id != yemek.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(yemek);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YemekExists(yemek.Id))
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
            ViewData["KategoriId"] = new SelectList(_context.Kategori, "KategoriId", "KategoriAdi", yemek.KategoriId);
            return View(yemek);
        }

        // GET: Yemek/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yemek = await _context.Yemek
                .Include(y => y.Kategori)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (yemek == null)
            {
                return NotFound();
            }

            return View(yemek);
        }

        // POST: Yemek/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var yemek = await _context.Yemek.FindAsync(id);
            _context.Yemek.Remove(yemek);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YemekExists(int id)
        {
            return _context.Yemek.Any(e => e.Id == id);
        }
    }
}
