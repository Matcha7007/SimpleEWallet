using System;
using System.Collections.Generic;

namespace SimpleEWallet.Wallet.Domain;

public partial class TrnTopupRequest
{
    public Guid Id { get; set; }

    public Guid WalletId { get; set; }

    public decimal Amount { get; set; }

    public int? StatusId { get; set; }

    public bool? IsActive { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? LastModifiedBy { get; set; }

    public DateTime? LastModifiedAt { get; set; }

    public virtual MstStatus? Status { get; set; }

    public virtual MstWallet Wallet { get; set; } = null!;
}
