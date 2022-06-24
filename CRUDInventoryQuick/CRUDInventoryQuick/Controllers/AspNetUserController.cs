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
    public class AspNetUserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AspNetUserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AspNetUser
        public async Task<IActionResult> Index()
        {
              return _context.ASPNETUSERs != null ? 
                          View(await _context.ASPNETUSERs.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ASPNETUSERs'  is null.");
        }

        // GET: AspNetUser/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.ASPNETUSERs == null)
            {
                return NotFound();
            }

            var aSPNETUSER = await _context.ASPNETUSERs
                .FirstOrDefaultAsync(m => m.ASPNETUSER_ID == id);
            if (aSPNETUSER == null)
            {
                return NotFound();
            }

            return View(aSPNETUSER);
        }

        // GET: AspNetUser/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AspNetUser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ASPNETUSER_ID,NombreNetUserId,Nombres,Apellidos,FechaNacimiento,Dirección,Teléfono,Correo,CorreoConfirmado,Contraseña,SelloDeSeguridad,ReclamarTelefono,DosFactoresDisponibles,FechaCierre,FechaAbierta,AccesoDenegado")] ASPNETUSER aSPNETUSER)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aSPNETUSER);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aSPNETUSER);
        }

        // GET: AspNetUser/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.ASPNETUSERs == null)
            {
                return NotFound();
            }

            var aSPNETUSER = await _context.ASPNETUSERs.FindAsync(id);
            if (aSPNETUSER == null)
            {
                return NotFound();
            }
            return View(aSPNETUSER);
        }

        // POST: AspNetUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("ASPNETUSER_ID,NombreNetUserId,Nombres,Apellidos,FechaNacimiento,Dirección,Teléfono,Correo,CorreoConfirmado,Contraseña,SelloDeSeguridad,ReclamarTelefono,DosFactoresDisponibles,FechaCierre,FechaAbierta,AccesoDenegado")] ASPNETUSER aSPNETUSER)
        {
            if (id != aSPNETUSER.ASPNETUSER_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aSPNETUSER);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ASPNETUSERExists(aSPNETUSER.ASPNETUSER_ID))
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
            return View(aSPNETUSER);
        }

        // GET: AspNetUser/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.ASPNETUSERs == null)
            {
                return NotFound();
            }

            var aSPNETUSER = await _context.ASPNETUSERs
                .FirstOrDefaultAsync(m => m.ASPNETUSER_ID == id);
            if (aSPNETUSER == null)
            {
                return NotFound();
            }

            return View(aSPNETUSER);
        }

        // POST: AspNetUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.ASPNETUSERs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ASPNETUSERs'  is null.");
            }
            var aSPNETUSER = await _context.ASPNETUSERs.FindAsync(id);
            if (aSPNETUSER != null)
            {
                _context.ASPNETUSERs.Remove(aSPNETUSER);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ASPNETUSERExists(decimal id)
        {
          return (_context.ASPNETUSERs?.Any(e => e.ASPNETUSER_ID == id)).GetValueOrDefault();
        }
    }
}
