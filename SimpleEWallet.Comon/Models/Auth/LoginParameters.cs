using SimpleEWallet.Comon.Base.WebAPI;

namespace SimpleEWallet.Comon.Models.Auth
{
    public class LoginParameters : BaseMethodParameters
	{
		public string Phone { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
	}
}
