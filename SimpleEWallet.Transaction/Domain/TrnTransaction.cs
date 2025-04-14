using System;
using System.Collections.Generic;

namespace SimpleEWallet.Transaction.Domain;

public partial class TrnTransaction
{
    public Guid Id { get; set; }

    public Guid WalletId { get; set; }

    public int TransactionTypeId { get; set; }

    public decimal Amount { get; set; }

    public string? Reference { get; set; }

    public bool? IsActive { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? LastModifiedBy { get; set; }

    public DateTime? LastModifiedAt { get; set; }
}
