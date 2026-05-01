using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceRequestMS.Application.DTOs;
using ServiceRequestMS.Application.Services.Interfaces;
using ServiceRequestMS.core.Models.Enums;
namespace ServiceRequestMS.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ItemController : ControllerBase
{
    IItemService _itemService;
    public ItemController(IItemService itemService)
    {
        _itemService = itemService;
    }

    [Authorize(Roles = $"{nameof(UserRoles.Admin)}")]
    [HttpPost("CreateItem")]
    public async Task<ActionResult> CreateItem(ItemDto dto)
    {
        var result = await _itemService.CreateItemAsync(dto);
        if(result.Success == false) return BadRequest(result);
        return Ok(result);
    }

    [HttpGet("GetItem/{Id}")]
    public async Task<ActionResult> GetItem(Guid Id)
    {
        var result = await _itemService.GetItemByIdAsync(Id);
        if (result.Success == false) return BadRequest(result);
        return Ok(result);
    }

    [HttpGet("GetAllItems")]
    public async Task<ActionResult> GetAllItems()
    {
        var result = await _itemService.GetAllItemAsync();
        if (result.Success == false) return BadRequest(result);
        return Ok(result);
    }
    [Authorize(Roles = $"{nameof(UserRoles.Admin)}")]
    [HttpPost("UpdateItems")]
    public async Task<ActionResult> UpdateItem(UpdateCategoryDto dto)
    {
        var result = await _itemService.UpdateItem(dto);
        if (result.Success == false) return BadRequest(result);
        return Ok(result);
    }

    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteItem(Guid Id)
    {
        var result = await _itemService.DeleteItem(Id);
        if (result.Success == false) return BadRequest(result);
        return Ok(result);
    }

}
