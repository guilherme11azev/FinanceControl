namespace FinanceControl.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    
    // Propriedade de navegação — EF Core usa isso para fazer os JOINs
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}