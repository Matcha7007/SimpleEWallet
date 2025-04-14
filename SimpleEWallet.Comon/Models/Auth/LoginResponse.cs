using SimpleEWallet.Comon.Base.WebAPI;

namespace SimpleEWallet.Comon.Models.Auth
{
    public class LoginResponse : BaseResponse
	{
		public string BearerToken { get; set; } = string.Empty;
	}
}
