using System;
using System.Collections.Generic;

namespace MachineTest_RESTAPI.Model;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<UserRegistration> UserRegistrations { get; set; } = new List<UserRegistration>();
}
