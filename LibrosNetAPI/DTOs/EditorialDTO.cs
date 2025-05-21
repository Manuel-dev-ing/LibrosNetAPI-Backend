using LibrosNetAPI.Entidades;

namespace LibrosNetAPI.DTOs
{
    public class EditorialDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Correo { get; set; }

        public string Telefono { get; set; }

        public string Calle { get; set; }

        public string Numero { get; set; }

        public string Colonia { get; set; }

        public string CodigoPostal { get; set; }

        public string Ciudad { get; set; }

        public string Estado { get; set; }

        public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();




    }
}
