using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace reviuAPI.Models;

public partial class CuntigutLliste
{
    public int ContingutLlistaId { get; set; }

    public int FkLlistaId { get; set; }

    public int FkContingutId { get; set; }

    public virtual Contingut FkContingut { get; set; } = null!;

    [JsonIgnore]
    public virtual Lliste FkLlista { get; set; } = null!;
}
