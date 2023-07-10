using System;
using System.Collections.Generic;

namespace APICineflex.Models.DB;

public partial class Resena
{
    public int IdResena { get; set; }

    public string Titulo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int IdPeliculaP { get; set; }

    public int IdUserP { get; set; }

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    public virtual Pelicula IdPeliculaPNavigation { get; set; } = null!;

    public virtual Usuario IdUserPNavigation { get; set; } = null!;
}
