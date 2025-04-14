using System;
using System.Collections.Generic;

namespace SimpleEWallet.Wallet.Domain;

public partial class MstStatus
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? LastModifiedBy { get; set; }

    public DateTime? LastModifiedAt { get; set; }

    public virtual ICollection<TrnTopupRequest> TrnTopupRequests { get; set; } = new List<TrnTopupRequest>();

    public virtual ICollection<TrnTransfer> TrnTransfers { get; set; } = new List<TrnTransfer>();
}
