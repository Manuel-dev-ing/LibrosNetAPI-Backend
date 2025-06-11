using System.ComponentModel.DataAnnotations;

namespace LibrosNetAPI.DTOs
{
    public class LibroCreacionDTO
    {
        [Required(ErrorMessage = "El campo autor es requerido")]
        public int IdAutor { get; set; }

        [Required(ErrorMessage = "El campo categoria es requerido")]
        public int IdCategoria { get; set; }

        [Required(ErrorMessage = "El campo editorial es requerido")]
        public int IdEditorial { get; set; }

        [Required(ErrorMessage = "El campo titulo es requerido")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "El campo precio es requerido")]
        public decimal Precio { get; set; }

        public int Stock { get; set; }

        [Required(ErrorMessage = "El campo portada es requerido")]
        public IFormFile Portada { get; set; }

        public string Isbn { get; set; }

        public DateTime FechaPublicacion { get; set; }

        public int NumeroPaginas { get; set; }

        public string Idioma { get; set; }

        public string Sipnosis { get; set; }


    }
}
