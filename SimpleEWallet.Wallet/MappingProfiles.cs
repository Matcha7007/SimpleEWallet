using AutoMapper;
using SimpleEWallet.Comon.Models.Wallet;
using SimpleEWallet.Wallet.Domain;

namespace SimpleEWallet.Wallet
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<MstWallet, WalletDto>()
				.ForMember(dst => dst.TopupRequests, opt => opt.MapFrom(src => src.TrnTopupRequests))
				.ForMember(dst => dst.TransferSenderWallets, opt => opt.MapFrom(src => src.TrnTransferSenderWallets))
				.ForMember(dst => dst.TransferReceiverWallets, opt => opt.MapFrom(src => src.TrnTransferReceiverWallets));
		}
	}
}
