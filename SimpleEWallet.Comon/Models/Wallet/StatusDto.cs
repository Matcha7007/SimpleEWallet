namespace SimpleEWallet.Comon.Models.Wallet;

public partial class StatusDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }   

    public virtual ICollection<TopupRequestDto> TopupRequests { get; set; } = [];

    public virtual ICollection<TransferDto> Transfers { get; set; } = [];
}
