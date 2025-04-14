namespace SimpleEWallet.Comon.Models.Wallet;

public partial class TransferDto
{
    public Guid Id { get; set; }

    public Guid SenderWalletId { get; set; }

    public Guid ReceiverWalletId { get; set; }

    public decimal Amount { get; set; }

    public string? Description { get; set; }

    public int? StatusId { get; set; }    

    public virtual WalletDto ReceiverWallet { get; set; } = null!;

    public virtual WalletDto SenderWallet { get; set; } = null!;

    public virtual StatusDto? Status { get; set; }
}
