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
    public class PrecioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PrecioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Precio
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PRECIOs.Include(p => p.PRODUCTO_Producto);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Precio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PRECIOs == null)
            {
                return NotFound();
            }

            var pRECIO = await _context.PRECIOs
                .Include(p => p.PRODUCTO_Producto)
                .FirstOrDefaultAsync(m => m.PrecioId == id);
            if (pRECIO == null)
            {
                return NotFound();
            }

            return View(pRECIO);
        }

        // GET: Precio/Create
        public IActionResult Create()
        {
            ViewData["PRODUCTO_ProductoId"] = new SelectList(_context.PRODUCTOs, "ProductoId", "ProductoId");
            return View();
        }

        // POST: Precio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrecioId,FechaIngreso,PrecioCompra,Descuento,PrecioVentaInicial,PrecioVentaFinal,PRODUCTO_ProductoId")] PRECIO pRECIO)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pRECIO);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PRODUCTO_ProductoId"] = new SelectList(_context.PRODUCTOs, "ProductoId", "ProductoId", pRECIO.PRODUCTO_ProductoId);
            return View(pRECIO);
        }

        // GET: Precio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PRECIOs == null)
            {
                return NotFound();
            }

            var pRECIO = await _context.PRECIOs.FindAsync(id);
            if (pRECIO == null)
            {
                return NotFound();
            }
            ViewData["PRODUCTO_ProductoId"] = new SelectList(_context.PRODUCTOs, "ProductoId", "ProductoId", pRECIO.PRODUCTO_ProductoId);
            return View(pRECIO);
        }

        // POST: Precio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PrecioId,FechaIngreso,PrecioCompra,Descuento,PrecioVentaInicial,PrecioVentaFinal,PRODUCTO_ProductoId")] PRECIO pRECIO)
        {
            if (id != pRECIO.PrecioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pRECIO);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PRECIOExists(pRECIO.PrecioId))
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
            ViewData["PRODUCTO_ProductoId"] = new SelectList(_context.PRODUCTOs, "ProductoId", "ProductoId", pRECIO.PRODUCTO_ProductoId);
            return View(pRECIO);
        }

        // GET: Precio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PRECIOs == null)
            {
                return NotFound();
            }

            var pRECIO = await _context.PRECIOs
                .Include(p => p.PRODUCTO_Producto)
                .FirstOrDefaultAsync(m => m.PrecioId == id);
            if (pRECIO == null)
            {
                return NotFound();
            }

            return View(pRECIO);
        }

        // POST: Precio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PRECIOs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PRECIOs'  is null.");
            }
            var pRECIO = await _context.PRECIOs.FindAsync(id);
            if (pRECIO != null)
            {
                _context.PRECIOs.Remove(pRECIO);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PRECIOExists(int id)
        {
          return (_context.PRECIOs?.Any(e => e.PrecioId == id)).GetValueOrDefault();
        }
    }
}
