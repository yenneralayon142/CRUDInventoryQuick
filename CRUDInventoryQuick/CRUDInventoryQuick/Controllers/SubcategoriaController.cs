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
    public class SubcategoriaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubcategoriaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Subcategoria
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SUBCATEGORIAs.Include(s => s.CATEGORIA_Categoria);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Subcategoria/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SUBCATEGORIAs == null)
            {
                return NotFound();
            }

            var sUBCATEGORIum = await _context.SUBCATEGORIAs
                .Include(s => s.CATEGORIA_Categoria)
                .FirstOrDefaultAsync(m => m.SubcategoriaId == id);
            if (sUBCATEGORIum == null)
            {
                return NotFound();
            }

            return View(sUBCATEGORIum);
        }

        // GET: Subcategoria/Create
        public IActionResult Create()
        {
            ViewData["CATEGORIA_CategoriaId"] = new SelectList(_context.CATEGORIAs, "CategoriaId", "CategoriaId");
            return View();
        }

        // POST: Subcategoria/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubcategoriaId,Nombre,Estado,CATEGORIA_CategoriaId")] SUBCATEGORIum sUBCATEGORIum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sUBCATEGORIum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CATEGORIA_CategoriaId"] = new SelectList(_context.CATEGORIAs, "CategoriaId", "CategoriaId", sUBCATEGORIum.CATEGORIA_CategoriaId);
            return View(sUBCATEGORIum);
        }

        // GET: Subcategoria/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SUBCATEGORIAs == null)
            {
                return NotFound();
            }

            var sUBCATEGORIum = await _context.SUBCATEGORIAs.FindAsync(id);
            if (sUBCATEGORIum == null)
            {
                return NotFound();
            }
            ViewData["CATEGORIA_CategoriaId"] = new SelectList(_context.CATEGORIAs, "CategoriaId", "CategoriaId", sUBCATEGORIum.CATEGORIA_CategoriaId);
            return View(sUBCATEGORIum);
        }

        // POST: Subcategoria/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubcategoriaId,Nombre,Estado,CATEGORIA_CategoriaId")] SUBCATEGORIum sUBCATEGORIum)
        {
            if (id != sUBCATEGORIum.SubcategoriaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sUBCATEGORIum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SUBCATEGORIumExists(sUBCATEGORIum.SubcategoriaId))
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
            ViewData["CATEGORIA_CategoriaId"] = new SelectList(_context.CATEGORIAs, "CategoriaId", "CategoriaId", sUBCATEGORIum.CATEGORIA_CategoriaId);
            return View(sUBCATEGORIum);
        }

        // GET: Subcategoria/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SUBCATEGORIAs == null)
            {
                return NotFound();
            }

            var sUBCATEGORIum = await _context.SUBCATEGORIAs
                .Include(s => s.CATEGORIA_Categoria)
                .FirstOrDefaultAsync(m => m.SubcategoriaId == id);
            if (sUBCATEGORIum == null)
            {
                return NotFound();
            }

            return View(sUBCATEGORIum);
        }

        // POST: Subcategoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SUBCATEGORIAs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SUBCATEGORIAs'  is null.");
            }
            var sUBCATEGORIum = await _context.SUBCATEGORIAs.FindAsync(id);
            if (sUBCATEGORIum != null)
            {
                _context.SUBCATEGORIAs.Remove(sUBCATEGORIum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SUBCATEGORIumExists(int id)
        {
          return (_context.SUBCATEGORIAs?.Any(e => e.SubcategoriaId == id)).GetValueOrDefault();
        }
    }
}
