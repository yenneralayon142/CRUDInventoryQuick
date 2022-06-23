using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDInventoryQuick.Models
{
    [Table("Usuario")]
    public class UsuarioRegistrado : IdentityUser
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Pais { get; set; }
        public string CodigoPais { get; set; }
        public string Ciudad { get; set; }
        public string Direccion { get; set; }
        public bool Estado { get; set; } = true;
        public string? URL { get; set; }

        public bool AccesoDenegado { get; set; }

        [NotMapped]
        [Display(Name = "Rol para el Usuario")]
        public string IdRol { get; set; }
        [NotMapped]
        public string Rol { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> ListaRoles { get; set; } 
    }
}
