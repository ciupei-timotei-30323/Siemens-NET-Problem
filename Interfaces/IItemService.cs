using Siemens.Internship2026.GradeBook.DTO;
namespace Siemens.Internship2026.GradeBook.Interfaces
{
    public interface IItemService
    {
        public Task<ItemCollectionDTO> GetAllActiveWithStatsAsync();

        public Task<IEnumerable<ItemDTO>> GetTopNActivePassingAsync(int n);
    }
}
