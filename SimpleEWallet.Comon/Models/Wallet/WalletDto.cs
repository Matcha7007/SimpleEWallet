namespace SimpleEWallet.Comon.Models.Wallet;

public partial class WalletDto
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public decimal Balance { get; set; }

    public bool? IsActive { get; set; }

    public string WalletNumber { get; set; } = null!;

    public string WalletName { get; set; } = null!;

    public virtual ICollection<TopupRequestDto> TopupRequests { get; set; } = [];

    public virtual ICollection<TransferDto> TransferReceiverWallets { get; set; } = [];

    public virtual ICollection<TransferDto> TransferSenderWallets { get; set; } = [];
}
