using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;
using System.Text.Json;

namespace Siemens.Internship2026.GradeBook.Repositories;

public class ItemRepository : IItemReader
{
    private readonly HttpClient _httpClient;
    private const string Url = "https://gist.githubusercontent.com/ArdeleanTudor/8ea407832cd9794960e0e6bbd1319f6e/raw/145b121103dd1cee3737a681c487f7295ac82e6b/gistfile1.txt";

    public ItemRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Item>> GetAllItemsAsync()
    {
        try
        {
            using var stream = await _httpClient.GetStreamAsync(Url);

            // parse json document directly from stream for better performance and memory efficiency
            using var doc = await JsonDocument.ParseAsync(stream);

            // use case-insensitive deserialization to handle potential casing issues in json properties
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            if (doc.RootElement.TryGetProperty("items", out var itemsElement))
            {
                var items = itemsElement.Deserialize<List<Item>>(options);
                return items?.Where(i => i.IsActive) ?? Enumerable.Empty<Item>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] {DateTime.UtcNow}: Failed to fetch items - {ex.Message}");
        }

        return Enumerable.Empty<Item>();
    }

    public async Task<Item?> GetItemByIdAsync(int id)
    {
        var items = await GetAllItemsAsync();
        return items.FirstOrDefault(i => i.Id == id);
    }
}