using System.ComponentModel.DataAnnotations;


namespace CRUDInventoryQuick.Models
{

    public class Producto
    {
        [Key]

        public int Id { get; set; } 
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public bool  Estado { get; set; }

        public string Subcategoria { get; set; }


        public string MarcaId { get; set; }
        

    }
}
