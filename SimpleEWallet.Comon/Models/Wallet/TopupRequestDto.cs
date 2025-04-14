namespace SimpleEWallet.Comon.Models.Wallet;

public partial class TopupRequestDto
{
    public Guid Id { get; set; }

    public Guid WalletId { get; set; }

    public decimal Amount { get; set; }

    public int? StatusId { get; set; }

    public virtual StatusDto? Status { get; set; }

    public virtual WalletDto Wallet { get; set; } = null!;
}
