using ServiceRequestMS.Application.Common;
using ServiceRequestMS.Application.DTOs;

namespace ServiceRequestMS.Application.Services.Interfaces
{
    public interface IItemService
    {
        Task<ApiResponse<ItemDto>> CreateItemAsync(ItemDto dto);
        Task<ApiResponse<ItemDto>> GetItemByIdAsync(Guid Id);
        Task<ApiResponse<IEnumerable<ItemDto>>> GetAllItemAsync();
        Task<ApiResponse<UpdateCategoryDto>> UpdateItem(UpdateCategoryDto Itemdto);
        Task<ApiResponse<bool>> DeleteItem(Guid Id);
    }
}
