using System;
using System.Collections.Generic;

namespace MachineTest_RESTAPI.Model;

public partial class PurchaseOrder
{
    public int PdId { get; set; }

    public string PdOrderNo { get; set; } = null!;

    public int AdId { get; set; }

    public int AtId { get; set; }

    public int? PdQty { get; set; }

    public int VdId { get; set; }

    public DateTime PdDate { get; set; }

    public string? PdStatus { get; set; }

    public virtual AssetDefinition Ad { get; set; } = null!;

    public virtual AssetType At { get; set; } = null!;

    public virtual Vendor Vd { get; set; } = null!;
}
