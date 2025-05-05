using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

namespace reviuAPI.Models;

public partial class Contingut
{
    public int ContingutId { get; set; }

    public double? Valoracio { get; set; }

    public int? TmdbId { get; set; }

    public string tipus { get; set; }

    public string? poster_path { get; set; }

    public virtual ICollection<Comentari> Comentaris { get; set; } = new List<Comentari>();

    [JsonIgnore]
    public virtual ICollection<CuntigutLliste> CuntigutLlistes { get; set; } = new List<CuntigutLliste>();

    [JsonIgnore]
    public virtual ICollection<Usuari> Usuaris { get; set; } = new List<Usuari>();

    [JsonIgnore]
    public virtual ICollection<Valoracio> Valoracios { get; set; } = new List<Valoracio>();
}
