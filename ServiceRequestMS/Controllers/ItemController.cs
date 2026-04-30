using Microsoft.AspNetCore.Mvc;
using ServiceRequestMS.Application.DTOs;
using ServiceRequestMS.Application.Services.Interfaces;
namespace ServiceRequestMS.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemController : ControllerBase
{
    IItemService _itemService;
    public ItemController(IItemService itemService)
    {
        _itemService = itemService;
    }

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
