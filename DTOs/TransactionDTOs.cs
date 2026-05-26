using FinanceControl.Models;

namespace FinanceControl.DTOs;

// O que o cliente envia para criar
public class TransactionCreateDTO
{
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public TransactionType Type { get; set; }
    public int CategoryId { get; set; }
}

// O que o cliente envia para editar
public class TransactionUpdateDTO
{
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public TransactionType Type { get; set; }
    public int CategoryId { get; set; }
}

// O que a API retorna (note: inclui CategoryName, não só o Id)
public class TransactionResponseDTO
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Type { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
}

// DTO para o resumo financeiro
public class FinancialSummaryDTO
{
    public decimal TotalIncome { get; set; }
    public decimal TotalExpense { get; set; }
    public decimal Balance { get; set; }
    public int TransactionCount { get; set; }
}