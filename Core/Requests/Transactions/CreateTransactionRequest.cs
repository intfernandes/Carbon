
using System.ComponentModel.DataAnnotations;

namespace Core.Requests.Transactions
{
    public class CreateTransactionRequest : Request
    {
        [Required(ErrorMessage = "Inserir título da transação")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Inserir tipo da transação")]
        public ETransactionType Type { get; set; } = ETransactionType.Withdraw;

        [Required(ErrorMessage = "Inserir categoria da transação")]
        public long CategoryId { get; set; }

        [Required(ErrorMessage = "Inserir valor da transação")]
        public decimal Amount { get; set; }
        public DateTime? PaidOrReceivedAt { get; set; }
    }
}