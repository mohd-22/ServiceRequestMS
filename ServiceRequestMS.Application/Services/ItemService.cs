using ServiceRequestMS.Application.Common;
using ServiceRequestMS.Application.DTOs;
using ServiceRequestMS.Application.Services.Interfaces;
using ServiceRequestMS.core.Models;
using ServiceRequestMS.Data.Repositories.Interfaces;
namespace ServiceRequestMS.Application.Services;
public class ItemService : IItemService
{
    private readonly IUnitOfWork _unitOfWork;
    public ItemService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;   

    public async Task<ApiResponse<ItemDto>> CreateItemAsync(ItemDto dto)
    {

        var categoryExists = await _unitOfWork.Categories.GetByIdAsync(dto.CategoryId);
        if (categoryExists == null)
        {
            return ApiResponse<ItemDto>.FailureResponse("Category not found!");
        }
        var item = new Item
        {
            Name = dto.Name,
            Description = dto.Description,
            CategoryId = dto.CategoryId
        };
        await _unitOfWork.Items.AddAsync(item);
        await _unitOfWork.CompleteAsync();
        return ApiResponse<ItemDto>.SuccessResponse(dto,"Item Created Succesfully");
    }
    public async Task<ApiResponse<ItemDto>> GetItemByIdAsync(Guid Id)
    {
        var item = await _unitOfWork.Items.GetByIdAsync(Id);
        if (item == null)
        {
            return ApiResponse<ItemDto>.FailureResponse("Item not found!");
        }

        var itemDto = new ItemDto
        {
            Name = item.Name,
            Description = item.Description,
            CategoryId = item.CategoryId
        };
        return ApiResponse<ItemDto>.SuccessResponse(itemDto,"Item Retrieved Succesfully");
    }        
    public async Task<ApiResponse<IEnumerable<ItemDto>>> GetAllItemAsync()
    {
        var items = await _unitOfWork.Items.GetAllAsync();

        var itemDtos = items.Select(item => new ItemDto
        {
            Name = item.Name,
            Description = item.Description,
            CategoryId = item.CategoryId
        });

        return ApiResponse<IEnumerable<ItemDto>>.SuccessResponse(itemDtos,"Items Retrieved Succesfully");
    }        
    public async Task<ApiResponse<UpdateCategoryDto>> UpdateItem(UpdateCategoryDto Itemdto)
    {
       
        var item = await _unitOfWork.Items.GetByIdAsync(Itemdto.Id);

        if (item == null)
        {
            return ApiResponse<UpdateCategoryDto>.FailureResponse("Item not found!");
        }

        item.Id = Itemdto.Id;
        item.Name = Itemdto.Name;
        item.Description = Itemdto.Description;

        _unitOfWork.Items.Update(item);
        await _unitOfWork.CompleteAsync();
        return ApiResponse<UpdateCategoryDto>.SuccessResponse(Itemdto,"Item Updated Succsefully");

    }
    public async Task<ApiResponse<bool>> DeleteItem(Guid Id)
    {
        var item = await _unitOfWork.Items.GetByIdAsync(Id);
        if (item == null) { return ApiResponse<bool>.FailureResponse("Item not found!");}

        var requests = await _unitOfWork.Requests.AnyAsync(x => x.CategoryItemId == Id);
        if(requests == true)
        {
            return ApiResponse<bool>.FailureResponse("There Are requests associated whit this item");
        }
        _unitOfWork.Items.Delete(item);
        await _unitOfWork.CompleteAsync();
        return ApiResponse<bool>.SuccessResponse(true,"Item deleted Succesfully");
    }
}
