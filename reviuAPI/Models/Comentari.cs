using System;
using System.Collections.Generic;

namespace reviuAPI.Models;

public partial class Comentari
{
    public int ComentariId { get; set; }

    public int LikesComentari { get; set; }

    public DateTime DataPublicacioComentari { get; set; }

    public int? EsResposta { get; set; }

    public int FkUsuariId { get; set; }

    public int? FkContingutId { get; set; }

    public int? FkValoracioId { get; set; }

    public virtual Contingut? FkContingut { get; set; }

    public virtual Usuari FkUsuari { get; set; } = null!;

    public virtual Valoracio? FkValoracio { get; set; }

    public string textComentari {  get; set; }

}
