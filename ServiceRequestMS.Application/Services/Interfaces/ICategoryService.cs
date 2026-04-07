using ServiceRequestMS.Application.Common;
using ServiceRequestMS.Application.DTOs;
using ServiceRequestMS.core.Models;

namespace ServiceRequestMS.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        // Read
        Task<ApiResponse<CategoryDto>> GetCategoryById(Guid categoryId);
        Task<ApiResponse<IEnumerable<CategoryItemDto>>> GetAllCategories();

        //Create
        Task<ApiResponse<CreateCategoryDto>> CreateCategory(CreateCategoryDto CatDto);

        //Update
        Task<ApiResponse<UpdateCategoryDto>> UpdateCategory(UpdateCategoryDto CatDto);

        //Delete
        Task<ApiResponse<bool>> DeleteCategory (Guid Id);
    }
}
