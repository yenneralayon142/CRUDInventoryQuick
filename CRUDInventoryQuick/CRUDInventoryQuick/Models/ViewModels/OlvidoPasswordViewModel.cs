using System.ComponentModel.DataAnnotations;

namespace CRUDInventoryQuick.Models.ViewModels
{
    public class OlvidoPasswordViewModel
    {
        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
