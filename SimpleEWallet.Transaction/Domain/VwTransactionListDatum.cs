using System;
using System.Collections.Generic;

namespace SimpleEWallet.Transaction.Domain;

public partial class VwTransactionListDatum
{
    public Guid? TransactionId { get; set; }

    public Guid? WalletId { get; set; }

    public int? TransactionTypeId { get; set; }

    public string? TransactionTypeName { get; set; }

    public decimal? Amount { get; set; }

    public string? CashFlow { get; set; }

    public string? Reference { get; set; }

    public DateTime? TransactionDate { get; set; }
}
