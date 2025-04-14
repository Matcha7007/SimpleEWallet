using System;
using System.Collections.Generic;

namespace SimpleEWallet.Auth.Domain;

public partial class MstConfig
{
    public int Id { get; set; }

    public string ConfigKey { get; set; } = null!;

    public string? ConfigValue { get; set; }

    public bool? IsActive { get; set; }

    public Guid? CreatedBy { get; set; }

    public TimeOnly? CreatedAt { get; set; }

    public Guid? LastModifiedBy { get; set; }

    public TimeOnly? LastModifiedAt { get; set; }
}
