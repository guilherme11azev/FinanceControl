namespace FinanceControl.DTOs;

// O que o cliente envia para criar/editar
public class CategoryCreateDTO
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}

// O que a API retorna
public class CategoryResponseDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}