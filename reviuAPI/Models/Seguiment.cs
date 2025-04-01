using System;
using System.Collections.Generic;

namespace reviuAPI.Models;

public partial class Seguiment
{
    public int SeguimentsId { get; set; }

    public int Segueix { get; set; }

    public int EsSeguit { get; set; }

    public virtual Usuari EsSeguitNavigation { get; set; } = null!;

    public virtual Usuari SegueixNavigation { get; set; } = null!;
}
