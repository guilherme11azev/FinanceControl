using FinanceControl.DTOs;

namespace FinanceControl.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponseDTO>> GetAllAsync();
    Task<CategoryResponseDTO?> GetByIdAsync(int id);
    Task<CategoryResponseDTO> CreateAsync(CategoryCreateDTO dto);
    Task<CategoryResponseDTO?> UpdateAsync(int id, CategoryCreateDTO dto);
    Task<bool> DeleteAsync(int id);
}