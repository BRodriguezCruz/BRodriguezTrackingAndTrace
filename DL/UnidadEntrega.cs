using System;
using System.Collections.Generic;

namespace DL;

public partial class UnidadEntrega
{
    public int IdUnidadEntrega { get; set; }

    public string NumeroPlaca { get; set; } = null!;

    public string Modelo { get; set; } = null!;

    public string Marca { get; set; } = null!;

    public DateTime AnioFabricacion { get; set; }

    public int IdEstatusUnidad { get; set; }

    public virtual EstatusUnidad IdEstatusUnidadNavigation { get; set; } = null!;

    public virtual ICollection<Repartidor> Repartidors { get; set; } = new List<Repartidor>();
}
