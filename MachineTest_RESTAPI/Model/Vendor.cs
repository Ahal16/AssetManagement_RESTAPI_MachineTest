using System;
using System.Collections.Generic;

namespace MachineTest_RESTAPI.Model;

public partial class Vendor
{
    public int VdId { get; set; }

    public string VdName { get; set; } = null!;

    public string VdType { get; set; } = null!;

    public int AtId { get; set; }

    public DateTime VdFrom { get; set; }

    public DateTime VdTo { get; set; }

    public string VdAddr { get; set; } = null!;

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<AssetMaster> AssetMasters { get; set; } = new List<AssetMaster>();

    public virtual AssetType At { get; set; } = null!;

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
}
