using System;
using System.Collections.Generic;

namespace LibrosNetAPI.Entidades;

public partial class Categorium
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
