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
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Login
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ASPNETUSERLOGINs.Include(a => a.ASPNETUSER_ASPNETUSER);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Login/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ASPNETUSERLOGINs == null)
            {
                return NotFound();
            }

            var aSPNETUSERLOGIN = await _context.ASPNETUSERLOGINs
                .Include(a => a.ASPNETUSER_ASPNETUSER)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aSPNETUSERLOGIN == null)
            {
                return NotFound();
            }

            return View(aSPNETUSERLOGIN);
        }

        // GET: Login/Create
        public IActionResult Create()
        {
            ViewData["ASPNETUSER_ASPNETUSER_ID"] = new SelectList(_context.ASPNETUSERs, "ASPNETUSER_ID", "ASPNETUSER_ID");
            return View();
        }

        // POST: Login/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LoginProvider,ProviderKey,ASPNETUSER_ASPNETUSER_ID")] ASPNETUSERLOGIN aSPNETUSERLOGIN)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aSPNETUSERLOGIN);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ASPNETUSER_ASPNETUSER_ID"] = new SelectList(_context.ASPNETUSERs, "ASPNETUSER_ID", "ASPNETUSER_ID", aSPNETUSERLOGIN.ASPNETUSER_ASPNETUSER_ID);
            return View(aSPNETUSERLOGIN);
        }

        // GET: Login/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ASPNETUSERLOGINs == null)
            {
                return NotFound();
            }

            var aSPNETUSERLOGIN = await _context.ASPNETUSERLOGINs.FindAsync(id);
            if (aSPNETUSERLOGIN == null)
            {
                return NotFound();
            }
            ViewData["ASPNETUSER_ASPNETUSER_ID"] = new SelectList(_context.ASPNETUSERs, "ASPNETUSER_ID", "ASPNETUSER_ID", aSPNETUSERLOGIN.ASPNETUSER_ASPNETUSER_ID);
            return View(aSPNETUSERLOGIN);
        }

        // POST: Login/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LoginProvider,ProviderKey,ASPNETUSER_ASPNETUSER_ID")] ASPNETUSERLOGIN aSPNETUSERLOGIN)
        {
            if (id != aSPNETUSERLOGIN.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aSPNETUSERLOGIN);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ASPNETUSERLOGINExists(aSPNETUSERLOGIN.Id))
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
            ViewData["ASPNETUSER_ASPNETUSER_ID"] = new SelectList(_context.ASPNETUSERs, "ASPNETUSER_ID", "ASPNETUSER_ID", aSPNETUSERLOGIN.ASPNETUSER_ASPNETUSER_ID);
            return View(aSPNETUSERLOGIN);
        }

        // GET: Login/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ASPNETUSERLOGINs == null)
            {
                return NotFound();
            }

            var aSPNETUSERLOGIN = await _context.ASPNETUSERLOGINs
                .Include(a => a.ASPNETUSER_ASPNETUSER)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aSPNETUSERLOGIN == null)
            {
                return NotFound();
            }

            return View(aSPNETUSERLOGIN);
        }

        // POST: Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ASPNETUSERLOGINs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ASPNETUSERLOGINs'  is null.");
            }
            var aSPNETUSERLOGIN = await _context.ASPNETUSERLOGINs.FindAsync(id);
            if (aSPNETUSERLOGIN != null)
            {
                _context.ASPNETUSERLOGINs.Remove(aSPNETUSERLOGIN);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ASPNETUSERLOGINExists(int id)
        {
          return (_context.ASPNETUSERLOGINs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
