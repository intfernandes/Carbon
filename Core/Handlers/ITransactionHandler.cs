using Core.Models;
using Core.Requests.Transactions;
using Core.Responses;

namespace Core.Handlers
{
    public interface ITransactionHandler
    {
        Task<Response<Transaction>> CreateAsync(CreateTransactionRequest request);
        Task<PagedResponse<List<Transaction>>> GetByPeriodAsync(GetTransactionsByPeriodRequest request);
        Task<Response<Transaction>> GetByIdAsync(GetTransactionByIdRequest request);
        Task<Response<Transaction>> UpdateAsync(UpdateTransactionRequest request);
        Task<Response<Transaction>> DeleteAsync(DeleteTransactionRequest request);
    }
}