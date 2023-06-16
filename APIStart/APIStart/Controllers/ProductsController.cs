using APIStart.DTOs.ProductDtos;
using APIStart.Exceptions.ProductExceptions;
using APIStart.Repositories.Interfaces;
using APIStart.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIStart.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? search)
    {
        return Ok(await _productService.GetAllProductsAsync(search));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var product = await _productService.GetProductByIdAsync(id);
            return Ok(product);
        }
        catch (ProductNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch(Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateDto productCreateDto)
    {
        try
        {
            var response = await _productService.CreateProductAsync(productCreateDto);
            return StatusCode((int)response.StatusCode, response.Message);
        }
        catch (ProductAlreadyExistException ex)
        {
            return StatusCode(StatusCodes.Status409Conflict, ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error");
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
    {
        try
        {
            var response = await _productService.UpdateProductAsync(productUpdateDto);
            return StatusCode((int)response.StatusCode, response.Message);
        }
        catch (ProductAlreadyExistException ex)
        {
            return StatusCode(StatusCodes.Status409Conflict, ex.Message);
        }
        catch(ProductNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var response = await _productService.DeleteProductAsync(id);
            return StatusCode((int)response.StatusCode, response.Message);
        }
        catch (ProductNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error");
        }
    }
}
