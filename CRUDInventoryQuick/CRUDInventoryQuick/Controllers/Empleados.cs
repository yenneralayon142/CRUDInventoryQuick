using Microsoft.AspNetCore.Mvc;

namespace CRUDInventoryQuick.Controllers
{
    public class Empleados : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
