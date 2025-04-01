using System;
using System.Collections.Generic;

namespace reviuAPI.Models;

public partial class Authentification
{
    public int AuthentificationId { get; set; }

    public string Correu { get; set; } = null!;

    public string? Contrasenya { get; set; }

    public int FkUsariId { get; set; }

    public virtual Usuari FkUsari { get; set; } = null!;
}
