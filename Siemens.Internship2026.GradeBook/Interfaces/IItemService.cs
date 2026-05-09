using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Interfaces;

public interface IItemService
{
    Task<IEnumerable<Item>> GetAllItemsAsync();
    Task<Item?> GetItemByIdAsync(int id);
    Task<IEnumerable<Item>> GetFilteredItemsAsync(int n);
    Task<ItemStatsResponse> GetItemStatisticsAsync();
}

// DTO for statistics response
public record ItemStatsResponse(IEnumerable<Item> Data, int TotalCount, decimal AverageValue);