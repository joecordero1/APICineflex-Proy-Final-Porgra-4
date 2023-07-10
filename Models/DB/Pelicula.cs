using System;
using System.Collections.Generic;

namespace APICineflex.Models.DB;

public partial class Pelicula
{
    public int IdPelicula { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string Genero { get; set; } = null!;

    public int Anio { get; set; }

    public string Poster { get; set; } = null!;

    public virtual ICollection<Resena> Resenas { get; set; } = new List<Resena>();
}
