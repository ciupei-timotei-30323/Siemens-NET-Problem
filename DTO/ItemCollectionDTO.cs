using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.DTO
{
    public class ItemCollectionDTO
    {
        public List<Item> Data { get; set; }
        public ItemStatisticsDTO Statistics { get; set; }
    }
}
