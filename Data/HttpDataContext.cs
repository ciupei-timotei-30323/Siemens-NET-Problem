using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;
using System.Text.Json;

namespace Siemens.Internship2026.GradeBook.Data
{
    public class HttpDataContext : IDataContext
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;

        public HttpDataContext(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _endpoint = configuration["ExternalApi:ItemsEndpoint"]
                ?? throw new InvalidOperationException("Items endpoint is not configured.");
        }

        private async Task<List<Item>> FetchItemsAsync()
        {


            var response = await _httpClient.GetAsync(_endpoint);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var items = JsonSerializer.Deserialize<List<Item>>(json, options);

            return items ?? new List<Item>();
        }

        public async Task<Item?> FirstOrDefaultAsync(Func<Item, bool> predicate)
        {
            var items = await FetchItemsAsync();
            return items.FirstOrDefault(predicate);
        }

        public async Task<IEnumerable<Item>> WhereAsync(Func<Item, bool> predicate)
        {
            var items = await FetchItemsAsync();
            return items.Where(predicate);
        }
    }
}
