using System;
using System.Collections.Generic;

namespace DL;

public partial class Entrega
{
    public int IdEntrega { get; set; }

    public DateTime FechaEntrega { get; set; }

    public int IdPaquete { get; set; }

    public int IdRepartidor { get; set; }

    public int IdEstatusEntrega { get; set; }

    public virtual EstatusEntrega IdEstatusEntregaNavigation { get; set; } = null!;

    public virtual Paquete IdPaqueteNavigation { get; set; } = null!;

    public virtual Repartidor IdRepartidorNavigation { get; set; } = null!;
}
