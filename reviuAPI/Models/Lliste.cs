using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace reviuAPI.Models;

public partial class Lliste
{
    public int LlistaId { get; set; }

    public string NomLlista { get; set; } = null!;

    public byte[]? FotoLlista { get; set; }

    public bool EsPublica { get; set; }

    public int FkUsuariId { get; set; }

    public virtual ICollection<CuntigutLliste> CuntigutLlistes { get; set; } = new List<CuntigutLliste>();

    [JsonIgnore]
    public virtual Usuari FkUsuari { get; set; } = null!;
}
