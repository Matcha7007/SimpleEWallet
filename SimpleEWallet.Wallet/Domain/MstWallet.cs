using System;
using System.Collections.Generic;

namespace SimpleEWallet.Wallet.Domain;

public partial class MstWallet
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public decimal Balance { get; set; }

    public bool? IsActive { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? LastModifiedBy { get; set; }

    public DateTime? LastModifiedAt { get; set; }

    public string WalletNumber { get; set; } = null!;

    public string WalletName { get; set; } = null!;

    public virtual ICollection<TrnTopupRequest> TrnTopupRequests { get; set; } = new List<TrnTopupRequest>();

    public virtual ICollection<TrnTransfer> TrnTransferReceiverWallets { get; set; } = new List<TrnTransfer>();

    public virtual ICollection<TrnTransfer> TrnTransferSenderWallets { get; set; } = new List<TrnTransfer>();
}
