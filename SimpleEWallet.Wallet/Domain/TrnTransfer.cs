using System;
using System.Collections.Generic;

namespace SimpleEWallet.Wallet.Domain;

public partial class TrnTransfer
{
    public Guid Id { get; set; }

    public Guid SenderWalletId { get; set; }

    public Guid ReceiverWalletId { get; set; }

    public decimal Amount { get; set; }

    public string? Description { get; set; }

    public int? StatusId { get; set; }

    public bool? IsActive { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? LastModifiedBy { get; set; }

    public DateTime? LastModifiedAt { get; set; }

    public Guid? TransactionId { get; set; }

    public virtual MstWallet ReceiverWallet { get; set; } = null!;

    public virtual MstWallet SenderWallet { get; set; } = null!;

    public virtual MstStatus? Status { get; set; }
}
