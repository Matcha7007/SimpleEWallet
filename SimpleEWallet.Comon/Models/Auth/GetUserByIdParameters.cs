using SimpleEWallet.Comon.Base.WebAPI;

namespace SimpleEWallet.Comon.Models.Auth
{
    public class GetUserByIdParameters : BaseMethodParameters
	{
		public Guid UserId { get; set; }
	}
}
