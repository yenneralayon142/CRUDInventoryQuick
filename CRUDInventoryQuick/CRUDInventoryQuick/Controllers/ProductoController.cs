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
    public class ProductoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Producto
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PRODUCTOs.Include(p => p.MARCA_Marca).Include(p => p.SUBCATEGORIA_Subcategoria);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Producto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PRODUCTOs == null)
            {
                return NotFound();
            }

            var pRODUCTO = await _context.PRODUCTOs
                .Include(p => p.MARCA_Marca)
                .Include(p => p.SUBCATEGORIA_Subcategoria)
                .FirstOrDefaultAsync(m => m.ProductoId == id);
            if (pRODUCTO == null)
            {
                return NotFound();
            }

            return View(pRODUCTO);
        }

        // GET: Producto/Create
        public IActionResult Create()
        {
            ViewData["MARCA_MarcaId"] = new SelectList(_context.MARCAs, "MarcaId", "MarcaId");
            ViewData["SUBCATEGORIA_SubcategoriaId"] = new SelectList(_context.SUBCATEGORIAs, "SubcategoriaId", "SubcategoriaId");
            return View();
        }

        // POST: Producto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductoId,Nombre,Descripción,Estado,SUBCATEGORIA_SubcategoriaId,MARCA_MarcaId")] PRODUCTO pRODUCTO)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pRODUCTO);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MARCA_MarcaId"] = new SelectList(_context.MARCAs, "MarcaId", "MarcaId", pRODUCTO.MARCA_MarcaId);
            ViewData["SUBCATEGORIA_SubcategoriaId"] = new SelectList(_context.SUBCATEGORIAs, "SubcategoriaId", "SubcategoriaId", pRODUCTO.SUBCATEGORIA_SubcategoriaId);
            return View(pRODUCTO);
        }

        // GET: Producto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PRODUCTOs == null)
            {
                return NotFound();
            }

            var pRODUCTO = await _context.PRODUCTOs.FindAsync(id);
            if (pRODUCTO == null)
            {
                return NotFound();
            }
            ViewData["MARCA_MarcaId"] = new SelectList(_context.MARCAs, "MarcaId", "MarcaId", pRODUCTO.MARCA_MarcaId);
            ViewData["SUBCATEGORIA_SubcategoriaId"] = new SelectList(_context.SUBCATEGORIAs, "SubcategoriaId", "SubcategoriaId", pRODUCTO.SUBCATEGORIA_SubcategoriaId);
            return View(pRODUCTO);
        }

        // POST: Producto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductoId,Nombre,Descripción,Estado,SUBCATEGORIA_SubcategoriaId,MARCA_MarcaId")] PRODUCTO pRODUCTO)
        {
            if (id != pRODUCTO.ProductoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pRODUCTO);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PRODUCTOExists(pRODUCTO.ProductoId))
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
            ViewData["MARCA_MarcaId"] = new SelectList(_context.MARCAs, "MarcaId", "MarcaId", pRODUCTO.MARCA_MarcaId);
            ViewData["SUBCATEGORIA_SubcategoriaId"] = new SelectList(_context.SUBCATEGORIAs, "SubcategoriaId", "SubcategoriaId", pRODUCTO.SUBCATEGORIA_SubcategoriaId);
            return View(pRODUCTO);
        }

        // GET: Producto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PRODUCTOs == null)
            {
                return NotFound();
            }

            var pRODUCTO = await _context.PRODUCTOs
                .Include(p => p.MARCA_Marca)
                .Include(p => p.SUBCATEGORIA_Subcategoria)
                .FirstOrDefaultAsync(m => m.ProductoId == id);
            if (pRODUCTO == null)
            {
                return NotFound();
            }

            return View(pRODUCTO);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PRODUCTOs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PRODUCTOs'  is null.");
            }
            var pRODUCTO = await _context.PRODUCTOs.FindAsync(id);
            if (pRODUCTO != null)
            {
                _context.PRODUCTOs.Remove(pRODUCTO);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PRODUCTOExists(int id)
        {
          return (_context.PRODUCTOs?.Any(e => e.ProductoId == id)).GetValueOrDefault();
        }
    }
}
