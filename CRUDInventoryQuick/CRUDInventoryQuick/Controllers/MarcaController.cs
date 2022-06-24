using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUDInventoryQuick.Datos;
using CRUDInventoryQuick.Models;

namespace CRUDInventoryQuick.Controllers
{
    public class MarcaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MarcaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Marca
        public async Task<IActionResult> Index()
        {
              return _context.MARCAs != null ? 
                          View(await _context.MARCAs.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.MARCAs'  is null.");
        }

        // GET: Marca/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MARCAs == null)
            {
                return NotFound();
            }

            var mARCA = await _context.MARCAs
                .FirstOrDefaultAsync(m => m.MarcaId == id);
            if (mARCA == null)
            {
                return NotFound();
            }

            return View(mARCA);
        }

        // GET: Marca/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Marca/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MarcaId,Nombre,Estado")] MARCA mARCA)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mARCA);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mARCA);
        }

        // GET: Marca/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MARCAs == null)
            {
                return NotFound();
            }

            var mARCA = await _context.MARCAs.FindAsync(id);
            if (mARCA == null)
            {
                return NotFound();
            }
            return View(mARCA);
        }

        // POST: Marca/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MarcaId,Nombre,Estado")] MARCA mARCA)
        {
            if (id != mARCA.MarcaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mARCA);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MARCAExists(mARCA.MarcaId))
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
            return View(mARCA);
        }

        // GET: Marca/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MARCAs == null)
            {
                return NotFound();
            }

            var mARCA = await _context.MARCAs
                .FirstOrDefaultAsync(m => m.MarcaId == id);
            if (mARCA == null)
            {
                return NotFound();
            }

            return View(mARCA);
        }

        // POST: Marca/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MARCAs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MARCAs'  is null.");
            }
            var mARCA = await _context.MARCAs.FindAsync(id);
            if (mARCA != null)
            {
                _context.MARCAs.Remove(mARCA);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MARCAExists(int id)
        {
          return (_context.MARCAs?.Any(e => e.MarcaId == id)).GetValueOrDefault();
        }
    }
}
