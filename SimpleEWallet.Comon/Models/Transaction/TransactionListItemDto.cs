using SimpleEWallet.Comon.Base.WebAPI;

namespace SimpleEWallet.Comon.Models.Transaction;

public partial class TransactionListItemDto : BaseDto
{
    public Guid? TransactionId { get; set; }

    public Guid? WalletId { get; set; }

    public int? TransactionTypeId { get; set; }

    public string? TransactionTypeName { get; set; }

    public decimal? Amount { get; set; }

	public string? AmountToDisplay { get; set; } = string.Empty;

	public string? CashFlow { get; set; }

    public string? Reference { get; set; }

    public DateTime? TransactionDate { get; set; }
}
