using ServiceRequestMS.Application.Common;
using ServiceRequestMS.Application.DTOs;
namespace ServiceRequestMS.Application.Services.Interfaces;
public interface ICategoryService
{
    Task<ApiResponse<CategoryDto>> GetCategoryById(Guid categoryId);
    Task<ApiResponse<IEnumerable<CategoryItemDto>>> GetAllCategories();
    Task<ApiResponse<CreateCategoryDto>> CreateCategory(CreateCategoryDto CatDto);
    Task<ApiResponse<UpdateCategoryDto>> UpdateCategory(UpdateCategoryDto CatDto);
    Task<ApiResponse<bool>> DeleteCategory (Guid Id);
}
