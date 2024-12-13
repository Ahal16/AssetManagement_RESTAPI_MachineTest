using System;
using System.Collections.Generic;

namespace MachineTest_RESTAPI.Model;

public partial class AssetDefinition
{
    public int AdId { get; set; }

    public string AdName { get; set; } = null!;

    public int AtId { get; set; }

    public string AdClass { get; set; } = null!;
    [System.Text.Json.Serialization.JsonIgnore]

    public virtual ICollection<AssetMaster> AssetMasters { get; set; } = new List<AssetMaster>();

    public virtual AssetType? At { get; set; } = null!;

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
}
