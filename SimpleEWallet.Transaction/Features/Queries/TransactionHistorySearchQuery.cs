using MediatR;
using SimpleEWallet.Comon.Models.Transaction;

namespace SimpleEWallet.Transaction.Features.Queries
{
	public record TransactionHistorySearchQuery(TransactionHistorySearchParameters Parameters) : IRequest<TransactionHistorySearchResponse>;
}
