using Microsoft.EntityFrameworkCore;
using FinanceControl.Data;
using FinanceControl.Models;

namespace FinanceControl.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Transaction>> GetAllAsync(DateTime? startDate, DateTime? endDate, int? categoryId)
    {
        // IQueryable permite construir a query aos poucos antes de executar no banco
        var query = _context.Transactions
            .Include(t => t.Category) // faz o JOIN com a tabela Category
            .AsQueryable();

        if (startDate.HasValue)
            query = query.Where(t => t.Date >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(t => t.Date <= endDate.Value);

        if (categoryId.HasValue)
            query = query.Where(t => t.CategoryId == categoryId.Value);

        return await query.OrderByDescending(t => t.Date).ToListAsync();
    }

    public async Task<Transaction?> GetByIdAsync(int id)
    {
        return await _context.Transactions
            .Include(t => t.Category)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Transaction> CreateAsync(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }

    public async Task<Transaction?> UpdateAsync(int id, Transaction transaction)
    {
        var existing = await _context.Transactions.FindAsync(id);
        if (existing is null) return null;

        existing.Description = transaction.Description;
        existing.Amount = transaction.Amount;
        existing.Date = transaction.Date;
        existing.Type = transaction.Type;
        existing.CategoryId = transaction.CategoryId;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction is null) return false;

        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<(decimal totalIncome, decimal totalExpense)> GetSummaryAsync(DateTime? startDate, DateTime? endDate)
    {
        var query = _context.Transactions.AsQueryable();

        if (startDate.HasValue)
            query = query.Where(t => t.Date >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(t => t.Date <= endDate.Value);

        var totalIncome = await query
            .Where(t => t.Type == TransactionType.Income)
            .SumAsync(t => t.Amount);

        var totalExpense = await query
            .Where(t => t.Type == TransactionType.Expense)
            .SumAsync(t => t.Amount);

        return (totalIncome, totalExpense);
    }
}