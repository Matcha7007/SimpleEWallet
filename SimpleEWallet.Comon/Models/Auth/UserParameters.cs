using SimpleEWallet.Comon.Base.WebAPI;

namespace SimpleEWallet.Comon.Models.Auth
{
    public class UserParameters : BaseMethodParameters
    {
		public Guid? UserId { get; set; }
		public string? Username { get; set; }
		public string? FullName { get; set; }
		public string? Email { get; set; }
		public string? Phone { get; set; }
		public string? Password { get; set; }
		public string? Pin { get; set; }
	}
}
