using AutoMapper;

using SimpleEWallet.Comon.Models.Transaction;
using SimpleEWallet.Transaction.Domain;
using SimpleEWallet.Comon.Extensions;

namespace SimpleEWallet.Transaction
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<VwTransactionListDatum, TransactionListItemDto>()
				.ForMember(dest => dest.AmountToDisplay,
					opt => opt.MapFrom(src =>
						src.Amount.HasValue
							? $"{(src.CashFlow == "Cash In" ? "+" : "-")}IDR{src.Amount.Value.ToDisplayMoney()}"
							: null));
		}
	}
}
