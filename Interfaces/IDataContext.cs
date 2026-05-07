using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Interfaces
{
    public interface IDataContext
    {
        Task<Item?> FirstOrDefaultAsync(Func<Item, bool> predicate);
        Task<IEnumerable<Item>> WhereAsync(Func<Item, bool> predicate);
    }
}
