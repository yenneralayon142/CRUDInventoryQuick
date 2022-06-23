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
    public class CategoriaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Categoria
        public async Task<IActionResult> Index()
        {
              return _context.CATEGORIAs != null ? 
                          View(await _context.CATEGORIAs.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.CATEGORIAs'  is null.");
        }

        // GET: Categoria/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CATEGORIAs == null)
            {
                return NotFound();
            }

            var cATEGORIum = await _context.CATEGORIAs
                .FirstOrDefaultAsync(m => m.CategoriaId == id);
            if (cATEGORIum == null)
            {
                return NotFound();
            }

            return View(cATEGORIum);
        }

        // GET: Categoria/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categoria/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoriaId,Estado,Nombre")] CATEGORIum cATEGORIum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cATEGORIum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cATEGORIum);
        }

        // GET: Categoria/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CATEGORIAs == null)
            {
                return NotFound();
            }

            var cATEGORIum = await _context.CATEGORIAs.FindAsync(id);
            if (cATEGORIum == null)
            {
                return NotFound();
            }
            return View(cATEGORIum);
        }

        // POST: Categoria/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoriaId,Estado,Nombre")] CATEGORIum cATEGORIum)
        {
            if (id != cATEGORIum.CategoriaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cATEGORIum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CATEGORIumExists(cATEGORIum.CategoriaId))
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
            return View(cATEGORIum);
        }

        // GET: Categoria/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CATEGORIAs == null)
            {
                return NotFound();
            }

            var cATEGORIum = await _context.CATEGORIAs
                .FirstOrDefaultAsync(m => m.CategoriaId == id);
            if (cATEGORIum == null)
            {
                return NotFound();
            }

            return View(cATEGORIum);
        }

        // POST: Categoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CATEGORIAs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CATEGORIAs'  is null.");
            }
            var cATEGORIum = await _context.CATEGORIAs.FindAsync(id);
            if (cATEGORIum != null)
            {
                _context.CATEGORIAs.Remove(cATEGORIum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CATEGORIumExists(int id)
        {
          return (_context.CATEGORIAs?.Any(e => e.CategoriaId == id)).GetValueOrDefault();
        }
    }
}
