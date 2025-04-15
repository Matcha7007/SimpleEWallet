using SimpleEWallet.Comon.Base.WebAPI;

namespace SimpleEWallet.Comon.Models.Auth
{
    public class VerifyPinParameters : BaseMethodParameters
    {
		public Guid UserId { get; set; }
		public string Pin { get; set; } = string.Empty;
	}
}
