using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace reviuAPI.Models;

public partial class Usuari
{
    public int UsuariId { get; set; }

    public string NomUsuari { get; set; } = null!;

    public byte[]? FotoUsuari { get; set; }

    public int Seguidors { get; set; }

    public int Seguits { get; set; }

    public int? FkContingutId { get; set; }

    [JsonIgnore]
    public virtual ICollection<Authentification> Authentifications { get; set; } = new List<Authentification>();

    public virtual ICollection<Comentari> Comentaris { get; set; } = new List<Comentari>();

    public virtual Contingut? FkContingut { get; set; }

    public virtual ICollection<Lliste> Llistes { get; set; } = new List<Lliste>();

    [JsonIgnore]
    public virtual ICollection<Seguiment> SeguimentEsSeguitNavigations { get; set; } = new List<Seguiment>();

    [JsonIgnore]
    public virtual ICollection<Seguiment> SeguimentSegueixNavigations { get; set; } = new List<Seguiment>();

    public virtual ICollection<Valoracio> Valoracios { get; set; } = new List<Valoracio>();
}
