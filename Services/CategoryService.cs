using FinanceControl.DTOs;
using FinanceControl.Models;
using FinanceControl.Repositories;

namespace FinanceControl.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CategoryResponseDTO>> GetAllAsync()
    {
        var categories = await _repository.GetAllAsync();
        return categories.Select(ToResponseDTO);
    }

    public async Task<CategoryResponseDTO?> GetByIdAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);
        return category is null ? null : ToResponseDTO(category);
    }

    public async Task<CategoryResponseDTO> CreateAsync(CategoryCreateDTO dto)
    {
        var category = new Category
        {
            Name = dto.Name,
            Description = dto.Description
        };

        var created = await _repository.CreateAsync(category);
        return ToResponseDTO(created);
    }

    public async Task<CategoryResponseDTO?> UpdateAsync(int id, CategoryCreateDTO dto)
    {
        var category = new Category
        {
            Name = dto.Name,
            Description = dto.Description
        };

        var updated = await _repository.UpdateAsync(id, category);
        return updated is null ? null : ToResponseDTO(updated);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    // Método privado de conversão — evita repetição de código
    private static CategoryResponseDTO ToResponseDTO(Category category) => new()
    {
        Id = category.Id,
        Name = category.Name,
        Description = category.Description
    };
}