

using Api.DbContext;
using Core;
using Core.Common;
using Core.Handlers;
using Core.Models;
using Core.Requests.Transactions;
using Core.Responses;
using Microsoft.EntityFrameworkCore; 

namespace Api.Handlers
{
    public class TransactionHandler(AuthDbContext context) : ITransactionHandler
    {
        public async Task<Response<Transaction>> CreateAsync(CreateTransactionRequest request)
        {
            if (request is { Type: ETransactionType.Withdraw, Amount: > 0 })
            {
                request.Amount *= -1;
            }

            try
            {
                var transaction = new Transaction
                {
                    UserId = request.UserId,
                    CategoryId = request.CategoryId,
                    CreatedAt = DateTime.UtcNow,
                    Amount = request.Amount,
                    PaidOrReceivedAt = request.PaidOrReceivedAt,
                    Title = request.Title,
                    Type = request.Type,
                };

                await context.Transactions.AddAsync(transaction);
                await context.SaveChangesAsync();
                return new Response<Transaction>(transaction, code: 201);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new Response<Transaction>(null, code: 500, message: ex.Message);

            }
        }

        public async Task<Response<Transaction>> DeleteAsync(DeleteTransactionRequest request)
        {
            try
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(t => t.Id == request.Id && t.UserId == request.UserId);

                if (transaction == null)
                    return new Response<Transaction>(null, code: 404, message: "Transação não encontrada");

                context.Transactions.Remove(transaction);
                await context.SaveChangesAsync();
                return new Response<Transaction>(transaction);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new Response<Transaction>(null, code: 500, message: "Transação não encontrada");


            }
        }



        public async Task<Response<Transaction>> GetByIdAsync(GetTransactionByIdRequest request)
        {
            try
            {
                var transaction = await context.Transactions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                return transaction is null
                ? new Response<Transaction>(data: null, code: 404, message: "Not Found")
                : new Response<Transaction>(data: transaction, message: "Success!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new Response<Transaction>(data: null, code: 404, message: "Internal Server Error");



            }
        }

        public async Task<PagedResponse<List<Transaction>>> GetByPeriodAsync(GetTransactionsByPeriodRequest request)
        {
            var now = DateTime.Now;
            request.StartDate ??= now.GetFirstDay(year: now.Year, month: now.Month);
            request.StartDate ??= now.GetLastDay(year: now.Year, month: now.Month);

            try
            {
                var query = context.Transactions
            .AsNoTracking()
            .Where(t =>
                t.PaidOrReceivedAt >= request.StartDate &&
                t.PaidOrReceivedAt <= request.EndDate &&
                t.UserId == request.UserId
                )
            .OrderBy(t => t.PaidOrReceivedAt);

                var transactions = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Transaction>>(
                     transactions,
                     count,
                     request.PageNumber,
                     request.PageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new PagedResponse<List<Transaction>>(
                    [],
                    0,
                    request.PageNumber,
                    request.PageSize);
            }
        }

        public async Task<Response<Transaction>> UpdateAsync(UpdateTransactionRequest request)
        {
            if (request is { Type: ETransactionType.Withdraw, Amount: > 0 })
            {
                request.Amount *= -1;
            }

            try
            {

                var transaction = await context.Transactions.FirstOrDefaultAsync(t => t.Id == request.Id && t.UserId == request.UserId);

                if (transaction == null)
                    return new Response<Transaction>(null, code: 404, message: "Transação não encontrada");



                transaction.CategoryId = request.CategoryId;
                transaction.Amount = request.Amount;
                transaction.Title = request.Title;
                transaction.Type = request.Type;
                transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;



                context.Transactions.Update(transaction);
                await context.SaveChangesAsync();
                return new Response<Transaction>(transaction);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new Response<Transaction>(null, code: 500, message: ex.Message);

            }
        }
    }
}