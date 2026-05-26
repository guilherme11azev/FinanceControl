using FinanceControl.DTOs;

namespace FinanceControl.Services;

public interface ITransactionService
{
    Task<IEnumerable<TransactionResponseDTO>> GetAllAsync(DateTime? startDate, DateTime? endDate, int? categoryId);
    Task<TransactionResponseDTO?> GetByIdAsync(int id);
    Task<TransactionResponseDTO> CreateAsync(TransactionCreateDTO dto);
    Task<TransactionResponseDTO?> UpdateAsync(int id, TransactionUpdateDTO dto);
    Task<bool> DeleteAsync(int id);
    Task<FinancialSummaryDTO> GetSummaryAsync(DateTime? startDate, DateTime? endDate);
}