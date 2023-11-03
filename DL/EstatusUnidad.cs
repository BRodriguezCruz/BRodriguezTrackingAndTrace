using System;
using System.Collections.Generic;

namespace DL;

public partial class EstatusUnidad
{
    public int IdEstatusUnidad { get; set; }

    public string Estatus { get; set; } = null!;

    public virtual ICollection<UnidadEntrega> UnidadEntregas { get; set; } = new List<UnidadEntrega>();
}
