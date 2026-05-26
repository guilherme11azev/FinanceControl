namespace FinanceControl.Models;

public enum TransactionType
{
    Income = 1,   // Receita
    Expense = 2   // Despesa
}

public class Transaction
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public TransactionType Type { get; set; }
    
    // Chave estrangeira
    public int CategoryId { get; set; }
    
    // Propriedade de navegação
    public Category Category { get; set; } = null!;
}