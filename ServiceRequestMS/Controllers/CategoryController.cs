using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceRequestMS.Application.DTOs;
using ServiceRequestMS.Application.Services;
using ServiceRequestMS.Application.Services.Interfaces;

namespace ServiceRequestMS.Api.Controllers;
//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("GetCategoryById/{id}")]
    public async Task<ActionResult> GetCategoryById(Guid id)
    {
        var result = await _categoryService.GetCategoryById(id);
        if (result.Success == false) {
            return BadRequest(result);
        }
        else
        {
            return Ok(result);
        }
    }
    [HttpGet("GetAllCategories")]
    public async Task<ActionResult> GetAllCategories()
    {
        var result = await _categoryService.GetAllCategories();
        if (result.Success == false)
        {
            return BadRequest(result);
        }
        else
        {
            return Ok(result);
        }
    }

    [HttpPost("CreateCategory")]
    public async Task<ActionResult> CreateCategory(CreateCategoryDto dto)
    {
        var result = await _categoryService.CreateCategory(dto);
        if (result.Success == false)
        {
            return BadRequest(result);
        }
        else
        {
            return Ok(result);
        }
    }

    [HttpPost("UpdateCategory")]
    public async Task<ActionResult> UpdateCategory(UpdateCategoryDto dto)
    {
        var result = await _categoryService.UpdateCategory(dto);
        if (result.Success == false)
        {
            return BadRequest(result);
        }
        else
        {
            return Ok(result);
        }
    }

    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteItem(Guid Id)
    {
        var result = await _categoryService.DeleteCategory(Id);
        if (result.Success == false)
        {
            return BadRequest(result);
        }
        else
        {
            return Ok(result);
        }
    }
}
