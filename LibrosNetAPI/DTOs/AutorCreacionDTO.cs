using System.ComponentModel.DataAnnotations;

namespace LibrosNetAPI.DTOs
{
    public class AutorCreacionDTO
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Telefono { get; set; }

        [Required]
        public string Correo { get; set; }


    }
}
