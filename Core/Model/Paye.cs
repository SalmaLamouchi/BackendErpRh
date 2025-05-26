using System;
using System.Collections.Generic;

namespace Core.Model;

public partial class Paye
{
    public int Id { get; set; }

    public int? PersonnelId { get; set; }

    public DateOnly MoisPaye { get; set; }

    public decimal Montant { get; set; }

    public string Statut { get; set; }

    public virtual Personnel Personnel { get; set; }
}
