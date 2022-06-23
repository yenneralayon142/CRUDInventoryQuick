using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUDInventoryQuick.Data;
using CRUDInventoryQuick.Models;



namespace CRUDInventoryQuick.Controllers
{

    public class ProductosController : Controller
    {

        private readonly ApplicationDbContext _context;

       public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Producto> colProductos = _context.Productos.Where(s => s.Estado == true);
            return View(colProductos);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: EmployeesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Producto producto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Productos.Add (producto);
                    _context.SaveChanges();
                    TempData["ResultOk"] = "Dato agregado satisactoriamente";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(producto);
            }
        }

        // GET: EmployeesController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }
            var productofromdb = _context.Productos.Find(id);

            if (productofromdb == null)
            {
                return NotFound();
            }
            return View(productofromdb);
        }


        // POST: EmployeesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Producto producto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    _context.SaveChanges();
                    TempData["ResultOk"] = "Dato actualizado satisfactoriamente";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
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
            return View(producto);
        }

        private bool ProductoExists(int id)
        {
            return (_context.Productos?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // GET: EmployeesController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }
            var productofromdb = _context.Productos.Find(id);

            if (productofromdb == null)
            {
                return NotFound();
            }
            return View(productofromdb);
        }

        // POST: EmployeesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var productofromdb = _context.Productos.Find(id);

            if (productofromdb == null)
            {
                return NotFound();
            }
            // _context.Employees.Remove(employeefromdb);   // Refactorizado para inactivar el usuario. No eliminarlo.
            productofromdb.Estado = false;
            _context.Productos.Update(productofromdb);
            _context.SaveChanges();
            TempData["ResultOk"] = "Dato eliminado satisfactoriamente";
            return RedirectToAction(nameof(Index));

        }



    }
}
