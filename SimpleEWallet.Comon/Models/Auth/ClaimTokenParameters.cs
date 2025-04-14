using Microsoft.AspNetCore.Mvc;

using SimpleEWallet.Comon.Base.WebAPI;

namespace SimpleEWallet.Comon.Models.Auth
{
    public class ClaimTokenParameters : BaseMethodParameters
	{
		public required ControllerBase ControllerBase { get; set; }
	}
}
