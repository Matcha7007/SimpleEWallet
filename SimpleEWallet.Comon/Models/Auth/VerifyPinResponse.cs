using SimpleEWallet.Comon.Base.WebAPI;

namespace SimpleEWallet.Comon.Models.Auth
{
    public class VerifyPinResponse : BaseResponse
	{
		public string Result { get; set; } = string.Empty;
	}
}
