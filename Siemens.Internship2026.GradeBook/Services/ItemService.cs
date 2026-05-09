using Siemens.Internship2026.GradeBook.Models;
using Siemens.Internship2026.GradeBook.Interfaces;

namespace Siemens.Internship2026.GradeBook.Services;

public class ItemService : IItemService, IItemReader
{
    private readonly IItemReader _reader;

    public ItemService(IItemReader reader)
    {
        _reader = reader;
    }

    public async Task<IEnumerable<Item>> GetAllItemsAsync()
    {
        return await _reader.GetAllItemsAsync();
    }

    public async Task<Item?> GetItemByIdAsync(int id)
    {
        return await _reader.GetItemByIdAsync(id);
    }

    public async Task<IEnumerable<Item>> GetFilteredItemsAsync(int n)
    {
        var items = await _reader.GetAllItemsAsync();
        return items
            .Where(i => i.IsActive && i.Value >= 5) 
            .Take(n); 
    }

    public async Task<ItemStatsResponse> GetItemStatisticsAsync()
    {
        var items = (await _reader.GetAllItemsAsync()).ToList();
        var total = items.Count;
        var average = items.Any() ? items.Average(i => i.Value) : 0;

        return new ItemStatsResponse(items, total, average);
    }
}