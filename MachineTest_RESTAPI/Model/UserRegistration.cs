using System;
using System.Collections.Generic;

namespace MachineTest_RESTAPI.Model;

public partial class UserRegistration
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public int Age { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual Role? Role { get; set; } = null!;

    public virtual ICollection<UserLogin> UserLogins { get; set; } = new List<UserLogin>();
}
