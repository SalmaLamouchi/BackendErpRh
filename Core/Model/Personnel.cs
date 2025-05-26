using System;
using System.Collections.Generic;

namespace Core.Model;

public partial class Personnel
{
    public int Id { get; set; }

    public string Nom { get; set; }

    public string Prenom { get; set; }

    public string Email { get; set; }

    public string Poste { get; set; }

    public decimal? Salaire { get; set; }

    public DateOnly DateEmbauche { get; set; }

    public virtual ICollection<Conge> Conges { get; set; } = new List<Conge>();

    public virtual ICollection<Paye> Payes { get; set; } = new List<Paye>();
}
