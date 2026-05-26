using FinanceControl.DTOs;
using FinanceControl.Models;
using FinanceControl.Repositories;

namespace FinanceControl.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ICategoryRepository _categoryRepository;

    public TransactionService(ITransactionRepository transactionRepository, ICategoryRepository categoryRepository)
    {
        _transactionRepository = transactionRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<TransactionResponseDTO>> GetAllAsync(DateTime? startDate, DateTime? endDate, int? categoryId)
    {
        var transactions = await _transactionRepository.GetAllAsync(startDate, endDate, categoryId);
        return transactions.Select(ToResponseDTO);
    }

    public async Task<TransactionResponseDTO?> GetByIdAsync(int id)
    {
        var transaction = await _transactionRepository.GetByIdAsync(id);
        return transaction is null ? null : ToResponseDTO(transaction);
    }

    public async Task<TransactionResponseDTO> CreateAsync(TransactionCreateDTO dto)
    {
        // Regra de negócio: categoria deve existir
        var categoryExists = await _categoryRepository.ExistsAsync(dto.CategoryId);
        if (!categoryExists)
            throw new ArgumentException($"Categoria com Id {dto.CategoryId} não encontrada.");

        // Regra de negócio: valor deve ser positivo
        if (dto.Amount <= 0)
            throw new ArgumentException("O valor da transação deve ser maior que zero.");

        var transaction = new Transaction
        {
            Description = dto.Description,
            Amount = dto.Amount,
            Date = dto.Date,
            Type = dto.Type,
            CategoryId = dto.CategoryId
        };

        var created = await _transactionRepository.CreateAsync(transaction);

        // Busca novamente para trazer o Category incluso
        var result = await _transactionRepository.GetByIdAsync(created.Id);
        return ToResponseDTO(result!);
    }

    public async Task<TransactionResponseDTO?> UpdateAsync(int id, TransactionUpdateDTO dto)
    {
        var categoryExists = await _categoryRepository.ExistsAsync(dto.CategoryId);
        if (!categoryExists)
            throw new ArgumentException($"Categoria com Id {dto.CategoryId} não encontrada.");

        if (dto.Amount <= 0)
            throw new ArgumentException("O valor da transação deve ser maior que zero.");

        var transaction = new Transaction
        {
            Description = dto.Description,
            Amount = dto.Amount,
            Date = dto.Date,
            Type = dto.Type,
            CategoryId = dto.CategoryId
        };

        var updated = await _transactionRepository.UpdateAsync(id, transaction);
        if (updated is null) return null;

        var result = await _transactionRepository.GetByIdAsync(updated.Id);
        return ToResponseDTO(result!);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _transactionRepository.DeleteAsync(id);
    }

    public async Task<FinancialSummaryDTO> GetSummaryAsync(DateTime? startDate, DateTime? endDate)
    {
        var (totalIncome, totalExpense) = await _transactionRepository.GetSummaryAsync(startDate, endDate);

        var allTransactions = await _transactionRepository.GetAllAsync(startDate, endDate, null);

        return new FinancialSummaryDTO
        {
            TotalIncome = totalIncome,
            TotalExpense = totalExpense,
            Balance = totalIncome - totalExpense,
            TransactionCount = allTransactions.Count()
        };
    }

    private static TransactionResponseDTO ToResponseDTO(Transaction t) => new()
    {
        Id = t.Id,
        Description = t.Description,
        Amount = t.Amount,
        Date = t.Date,
        Type = t.Type.ToString(),        // Converte o enum para string legível
        CategoryId = t.CategoryId,
        CategoryName = t.Category.Name
    };
}