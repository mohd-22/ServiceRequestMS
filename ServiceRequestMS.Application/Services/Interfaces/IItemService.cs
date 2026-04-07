using ServiceRequestMS.Application.Common;
using ServiceRequestMS.Application.DTOs;

namespace ServiceRequestMS.Application.Services.Interfaces
{
    public interface IItemService
    {
        //Create
        Task<ApiResponse<ItemDto>> CreateItemAsync(ItemDto dto);

        //Read
        Task<ApiResponse<ItemDto>> GetItemByIdAsync(Guid Id);
        Task<ApiResponse<IEnumerable<ItemDto>>> GetAllItemAsync();

        //Update
        Task<ApiResponse<UpdateCategoryDto>> UpdateItem(UpdateCategoryDto Itemdto);

        //Delete
        Task<ApiResponse<bool>> DeleteItem(Guid Id);
    }
}
