using System;
using System.Collections.Generic;

namespace APICineflex.Models.DB;

public partial class Comentario
{
    public int IdComentario { get; set; }

    public int IdResenaF { get; set; }

    public int IdUserF { get; set; }

    public string Cuerpo { get; set; } = null!;

    public DateTime FechaComentario { get; set; }

    public virtual Resena IdResenaFNavigation { get; set; } = null!;

    public virtual Usuario IdUserFNavigation { get; set; } = null!;
}
