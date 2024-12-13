using System;
using System.Collections.Generic;

namespace MachineTest_RESTAPI.Model;

public partial class UserLogin
{
    public int LoginId { get; set; }

    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public bool? IsActive { get; set; }

    public virtual UserRegistration User { get; set; } = null!;
}
