using SimpleEWallet.Comon.Base.WebAPI;

namespace SimpleEWallet.Comon.Models
{
	public class ConfigDto(int configId, string configKey, string configValue, bool isActive) : BaseDto
	{
		public int ConfigId { get; set; } = configId;
		public string ConfigKey { get; set; } = configKey;
		public string? ConfigValue { get; set; } = configValue;
		public bool IsActive { get; set; } = isActive;
	}
}
