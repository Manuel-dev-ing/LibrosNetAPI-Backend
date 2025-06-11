namespace LibrosNetAPI.DTOs
{
    public class RegistroCreacionDTO
    {
        public int? IdRol { get; set; }

        public string? Nombre { get; set; }

        public string? PrimerApellido { get; set; }

        public string? SegundoApellido { get; set; }

        public string? Correo { get; set; }

        public string? Contrasena { get; set; }

        public string RepetirContrasena { get; set; }

        // crear validaciones para la contrasenas 

    }
}
