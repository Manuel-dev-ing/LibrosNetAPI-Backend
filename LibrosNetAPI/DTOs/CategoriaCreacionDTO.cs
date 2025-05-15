using System.ComponentModel.DataAnnotations;

namespace LibrosNetAPI.DTOs
{
    public class CategoriaCreacionDTO
    {
        [Required]
        public string Nombre { get; set; }

    }
}
