using System;
using System.Collections.Generic;

namespace LibrosNetAPI.Entidades;

public partial class Usuario
{
    public int Id { get; set; }

    public int? IdRol { get; set; }

    public string? Nombre { get; set; }

    public string? PrimerApellido { get; set; }

    public string? SegundoApellido { get; set; }

    public string? Correo { get; set; }

    public string? PasswordHash { get; set; }

    public virtual Rol? IdRolNavigation { get; set; }
}
