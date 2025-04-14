using AutoMapper;

using SimpleEWallet.Auth.Domain;
using SimpleEWallet.Comon.Models.Auth;

namespace SimpleEWallet.Auth.Tools
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<MstUser, UserDto>().
				ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));
			CreateMap<MstUser, UserReceiverDto>().ReverseMap();
		}
	}
}
