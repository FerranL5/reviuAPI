using System;
using System.Collections.Generic;

namespace reviuAPI.Models;

public partial class Valoracio
{
    public int ValoracioId { get; set; }

    public int Puntuacio { get; set; }

    public int LikesValoracio { get; set; }

    public DateTime DataPublicacioValoracio { get; set; }

    public int FkUsuariId { get; set; }

    public int FkContingutId { get; set; }

    public virtual ICollection<Comentari> Comentaris { get; set; } = new List<Comentari>();

    public virtual Contingut FkContingut { get; set; } = null!;

    public virtual Usuari FkUsuari { get; set; } = null!;
}
