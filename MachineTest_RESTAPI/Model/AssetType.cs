using System;
using System.Collections.Generic;

namespace MachineTest_RESTAPI.Model;

public partial class AssetType
{
    public int AtId { get; set; }

    public string AtName { get; set; } = null!;

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<AssetDefinition> AssetDefinitions { get; set; } = new List<AssetDefinition>();
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<AssetMaster> AssetMasters { get; set; } = new List<AssetMaster>();

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();

    public virtual ICollection<Vendor> Vendors { get; set; } = new List<Vendor>();
}
