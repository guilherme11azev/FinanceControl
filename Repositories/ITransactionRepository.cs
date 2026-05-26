using FinanceControl.Models;

namespace FinanceControl.Repositories;

public interface ITransactionRepository
{
    Task<IEnumerable<Transaction>> GetAllAsync(DateTime? startDate, DateTime? endDate, int? categoryId);
    Task<Transaction?> GetByIdAsync(int id);
    Task<Transaction> CreateAsync(Transaction transaction);
    Task<Transaction?> UpdateAsync(int id, Transaction transaction);
    Task<bool> DeleteAsync(int id);
    Task<(decimal totalIncome, decimal totalExpense)> GetSummaryAsync(DateTime? startDate, DateTime? endDate);
}