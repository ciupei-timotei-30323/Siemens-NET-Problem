using Microsoft.AspNetCore.Mvc;
using Siemens.Internship2026.GradeBook.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Siemens.Internship2026.GradeBook.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private readonly IItemReader _reader;
    private readonly IItemService _itemService;
    private readonly IItemValidatorService _validatorService;
    private readonly ILogger<ItemController> _logger;

    public ItemController(IItemService itemService ,IItemReader reader, ILogger<ItemController> logger, IItemValidatorService validatorService)
    {
        _reader = reader;
        _logger = logger;
        _validatorService = validatorService;
        _itemService = itemService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation($"[LOG] {DateTime.UtcNow}: GET api/item called");

        var response = await _itemService.GetAllActiveWithStatsAsync();
        _logger.LogInformation($"[LOG] Returning {response.Statistics.totalCount} items, average value: {response.Statistics.averageValue}");

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        _logger.LogInformation($"[LOG] {DateTime.UtcNow}: GET api/item/{id} called");

        var (isValid, error) = _validatorService.ValidateId(id);
        if (!isValid)
        {
            _logger.LogError($"[LOG] Invalid id: {id}");
            return BadRequest(error);
        }

        var item = await _reader.GetByIdActiveAsync(id);
        if (item == null)
        {
            _logger.LogError($"[LOG] Item {id} not found");
            return NotFound($"Item with Id {id} was not found.");
        }

        return Ok(item);
    }

    [HttpGet("passing")]
    public async Task<IActionResult> GetTopPassingGrade([FromQuery] int n)
    {
        _logger.LogInformation($"[LOG] {DateTime.UtcNow}: GET api/item/passing?n={n} called");
        var response = await _itemService.GetTopNActivePassingAsync(n);
        return Ok(response);

    }
}
