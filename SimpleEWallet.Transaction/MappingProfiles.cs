using AutoMapper;

using SimpleEWallet.Comon.Models.Transaction;
using SimpleEWallet.Transaction.Domain;

namespace SimpleEWallet.Transaction
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<VwTransactionListDatum, TransactionListItemDto>().ReverseMap();
		}
	}
}
