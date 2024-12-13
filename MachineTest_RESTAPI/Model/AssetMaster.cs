using System;
using System.Collections.Generic;

namespace MachineTest_RESTAPI.Model;

public partial class AssetMaster
{
    public int AmId { get; set; }

    public int AtId { get; set; }

    public int VdId { get; set; }

    public int AdId { get; set; }

    public string AmModel { get; set; } = null!;

    public string AmSnumber { get; set; } = null!;

    public string? AmMyyear { get; set; }

    public DateTime AmPdate { get; set; }

    public string? AmWarranty { get; set; }

    public DateTime AmFrom { get; set; }

    public DateTime AmTo { get; set; }

    public virtual AssetDefinition? Ad { get; set; } = null!;

    public virtual AssetType? At { get; set; } = null!;

    public virtual Vendor? Vd { get; set; } = null!;
}
