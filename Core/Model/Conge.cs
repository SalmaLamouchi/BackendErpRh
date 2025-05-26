using System;
using System.Collections.Generic;

namespace Core.Model;

public partial class Conge
{
    public int Id { get; set; }

    public int? PersonnelId { get; set; }

    public DateOnly DateDebut { get; set; }

    public DateOnly DateFin { get; set; }

    public string TypeConge { get; set; }

    public string Statut { get; set; }

    public virtual Personnel Personnel { get; set; }
}
