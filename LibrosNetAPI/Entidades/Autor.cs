using System;
using System.Collections.Generic;

namespace LibrosNetAPI.Entidades;

public partial class Autor
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? PrimerApellido { get; set; }

    public string? SegundoApellido { get; set; }

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
