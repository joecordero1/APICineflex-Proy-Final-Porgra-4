using System;
using System.Collections.Generic;

namespace APICineflex.Models.DB;

public partial class Usuario
{
    public int IdUser { get; set; }

    public int Rol { get; set; }

    public string Nombre { get; set; } = null!;

    public string NombreUsuario { get; set; } = null!;

    public string Contrasenia { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    public virtual ICollection<Resena> Resenas { get; set; } = new List<Resena>();
}
