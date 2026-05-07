using Siemens.Internship2026.GradeBook.Models;
namespace Siemens.Internship2026.GradeBook.Interfaces;

public interface IItemReader
{
    Task<Item?> GetByIdActiveAsync(int id);
    Task<IEnumerable<Item>> GetAllActiveAsync();
}
