using ServiceRequestMS.Application.Common;
using ServiceRequestMS.Application.DTOs;
using ServiceRequestMS.Application.Services.Interfaces;
using ServiceRequestMS.core.Models;
using ServiceRequestMS.core.Models.Enums;
using ServiceRequestMS.Data.Repositories.Interfaces;

namespace ServiceRequestMS.Application.Services;
public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
    public async Task<ApiResponse<CategoryDto>> GetCategoryById(Guid categoryId)
    {
        
        var category = await _unitOfWork.Categories.GetCategoryWithItemsAsync(categoryId);

        if (category == null) { return ApiResponse<CategoryDto>.FailureResponse("Category not found!"); }
        var CategoryDto = new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            Items = category.Items.Select(i => new CategoryItemDto
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description
            }).ToList()



        };
         return ApiResponse<CategoryDto>.SuccessResponse(CategoryDto, "Category retrieved successfully");
    }
    public async Task<ApiResponse<IEnumerable<CategoryItemDto>>> GetAllCategories()
    {
       var categories = await _unitOfWork.Categories.GetAllAsync();
        var categoriesDtos = categories.Select(category => new CategoryItemDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        });
        return ApiResponse<IEnumerable<CategoryItemDto>>.SuccessResponse(categoriesDtos,"Categories Retrieved Succesfully");
    }

    public async Task<ApiResponse<CreateCategoryDto>> CreateCategory(CreateCategoryDto CatDto)
    {
        var category = await _unitOfWork.Categories.AnyAsync(x => x.Name == CatDto.Name);
        if (category == true)
        {
            return ApiResponse<CreateCategoryDto>.FailureResponse("Category Already Exists!");
        }
        var NewCategory = new Category
        {
            Name = CatDto.Name,
            Description = CatDto.Description,
        };

        await _unitOfWork.Categories.AddAsync(NewCategory);
        await _unitOfWork.CompleteAsync();
        return ApiResponse<CreateCategoryDto>.SuccessResponse(CatDto,"Category Created Succesfully");

    }
    public async Task<ApiResponse<UpdateCategoryDto>> UpdateCategory(UpdateCategoryDto CatDto)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(CatDto.Id);
        if (category == null)
        {
            return ApiResponse<UpdateCategoryDto>.FailureResponse("Category not found!");
        }

        category.Name = CatDto.Name;
        category.Description = CatDto.Description;

        _unitOfWork.Categories.Update(category);
        await _unitOfWork.CompleteAsync();
        return ApiResponse<UpdateCategoryDto>.SuccessResponse(CatDto,"Categroy Updated Succesfully");

    }

    public async Task<ApiResponse<bool>> DeleteCategory(Guid Id)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(Id);
        if (category == null) { return ApiResponse<bool>.FailureResponse("Category not found!"); }

        var hasLinkedRequests = await _unitOfWork.Requests.AnyAsync(r =>
            r.CategoryItem.CategoryId == Id &&
            r.Status != RequestStatus.Accepted &&
            r.Status != RequestStatus.Rejected);
        if (hasLinkedRequests)
        {
             return ApiResponse<bool>.FailureResponse("There Are requests associated with this category!");
        }
        _unitOfWork.Categories.Delete(category);
        await _unitOfWork.CompleteAsync();
        return ApiResponse<bool>.SuccessResponse(true,"Category Deleted Succesfully");
    }
}
