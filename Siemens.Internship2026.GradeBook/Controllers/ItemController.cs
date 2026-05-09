using Microsoft.AspNetCore.Mvc;
using Siemens.Internship2026.GradeBook.Interfaces;

namespace Siemens.Internship2026.GradeBook.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private readonly IItemService _itemService;

    public ItemController(IItemService itemService)
    {
        _itemService = itemService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllItems()
    {
        Console.WriteLine($"[LOG] {DateTime.UtcNow}: GET api/item called");

        ItemStatsResponse stats = await _itemService.GetItemStatisticsAsync();

        Console.WriteLine($"[LOG] Returning {stats.TotalCount} items, average value: {stats.AverageValue}");

        return Ok(stats);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetItemById(int id)
    {
        Console.WriteLine($"[LOG] {DateTime.UtcNow}: GET api/item/{id} called");

        if (id <= 0) 
        {
            Console.WriteLine($"[LOG] Invalid id: {id}");
            return BadRequest("Id must be a positive integer.");
        }

        var item = await _itemService.GetItemByIdAsync(id);

        if (item == null)
        {
            Console.WriteLine($"[LOG] Item {id} not found");
            return NotFound($"Item with Id {id} was not found.");
        }
        return Ok(item);
    }

    [HttpGet("filter/{n}")]
    public async Task<IActionResult> GetFilteredItems(int n)
    {
        Console.WriteLine($"[LOG] {DateTime.UtcNow}: GET api/item/filter/{n} called");

        var results = await _itemService.GetFilteredItemsAsync(n);
        return Ok(results);
    }
}
