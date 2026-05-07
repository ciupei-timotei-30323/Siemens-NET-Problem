using Siemens.Internship2026.GradeBook.DTO;
using Siemens.Internship2026.GradeBook.Interfaces;
namespace Siemens.Internship2026.GradeBook.Services
{
    public class ItemService : IItemService
    {

        private readonly IItemReader _reader;

        public ItemService(IItemReader reader)
        {
            _reader = reader;
        }

        public async Task<ItemCollectionDTO> GetAllActiveWithStatsAsync()
        {
            var items = await _reader.GetAllActiveAsync();
            var itemList = items.ToList();

            var totalCount = itemList.Count;
            var averageValue = itemList.Any() ? itemList.Average(i => i.Value) : 0;

            return new ItemCollectionDTO
            {
                Data = itemList,
                Statistics = new ItemStatisticsDTO
                {
                    totalCount = totalCount,
                    averageValue = averageValue,
                    RetrievedAt = DateTime.UtcNow
                }
            };
        }


        public async Task<IEnumerable<ItemDTO>> GetTopNActivePassingAsync(int n)
        {
            if(n <= 0) { return Enumerable.Empty<ItemDTO>(); }

            var items = await _reader.GetAllActiveAsync(); 
            var itemsList = items.ToList();

            var response = itemsList.Where
                (i => i.Value >= 5)
                .Take(n)
                .Select(i => new ItemDTO
                {
                    Value = i.Value,
                    IsActive = i.IsActive,
                })
                .ToList();

            return response;
                
        }
    }
}
