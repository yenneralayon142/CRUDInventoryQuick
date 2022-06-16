using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CRUDInventoryQuick.Models.ViewModels;
using CRUDInventoryQuick.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using CRUDInventoryQuick.Data;

namespace CRUDInventoryQuick.Controllers
{ 


      [Authorize(Roles = "Administrador")]
    public class RolesController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public RolesController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = applicationDbContext;
        }

        //Lista de Roles
        public IActionResult Index()
        {
            return View();
        }


        //Crear Roles
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(IdentityRole identityRole)
        {
            if (ModelState.IsValid)
            {
                return View(identityRole);

            }

            if (await _roleManager.RoleExistsAsync(identityRole.Name))
            {
                TempData["Error"] = "El error ya existe";
                return RedirectToAction(nameof(Index));

            }

            //Crear el Rol
            await _roleManager.CreateAsync(new IdentityRole() { Name = identityRole.Name });
            TempData["Correcto"] = "Rol creado correctamente";
            return RedirectToAction(nameof(Index));
        }




        //editar rol
        [HttpGet]
        public IActionResult Editar(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View();
            }
            else
            {

                var rol = _context.Roles.FirstOrDefault(r => r.Id == id);
                //considerar que pasa si existe en la db
                return View(rol);
            }

        }

        //Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(IdentityRole roleManager)
        {

            if (await _roleManager.RoleExistsAsync(roleManager.Name))
            {
                TempData["Error"] = "El rol ya existe";
                return RedirectToAction(nameof(Index));
            }

            //Actualiza el rol
            var roleDB = _context.Roles.FirstOrDefault(r => r.Id == roleManager.Id);
            if (roleDB == null)
            {
                return RedirectToAction(nameof(Index));
            }

            roleDB.Name = roleManager.Name;
            roleDB.NormalizedName = roleManager.Name.ToUpper();
            var resultado = await _roleManager.UpdateAsync(roleDB);
            TempData["Correcto"] = "Rol editado correctamente";
            return RedirectToAction(nameof(Index));
        }

        //Borrar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Borrar(string id)
        {
            var rol = _context.Roles.FirstOrDefault(r => r.Id == id);
            if (rol == null)
            {
                TempData["Error"] = "No existe el Rol";
                return RedirectToAction(nameof(Index));

            }

            var usuariosRol = _context.UserRoles.Where(u => u.RoleId == id).Count();

            if (usuariosRol > 0)
            {
                TempData["Error"] = "El rol tiene ususarios no se puede borrar";
                return RedirectToAction(nameof(Index));

            }

            await _roleManager.DeleteAsync(rol); //Borrar rol de la DB
            return RedirectToAction(nameof(Index));
        }

    }
}





