namespace LibrosNetAPI.DTOs
{
    public class LibroDTO
    {
        public int Id { get; set; }

        public int IdAutor { get; set; }

        public int IdCategoria { get; set; }

        public int IdEditorial { get; set; }

        public string Titulo { get; set; }

        public decimal Precio { get; set; }

        public int Stock { get; set; }

        public string Portada { get; set; }

        public string Isbn { get; set; }

        public DateTime FechaPublicacion { get; set; }

        public int NumeroPaginas { get; set; }

        public string Idioma { get; set; }

        public string Sipnosis { get; set; }

        public bool Estado { get; set; }


    }
}
