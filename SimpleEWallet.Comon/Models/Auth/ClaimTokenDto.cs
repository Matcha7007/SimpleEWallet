using SimpleEWallet.Comon.Base.WebAPI;

namespace SimpleEWallet.Comon.Models.Auth;

public class ClaimTokenDto : BaseDto
{
	public Guid UserId { get; set; }
	public string Token { get; set; } = string.Empty;
    public bool IsTokenValid { get; set; }
}
