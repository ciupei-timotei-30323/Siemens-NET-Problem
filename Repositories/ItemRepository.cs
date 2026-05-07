using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;
namespace Siemens.Internship2026.GradeBook.Repositories;

public class ItemRepository : IItemReader
{
    private readonly IDataContext _context;

    // Dependency Injection
    public ItemRepository(IDataContext context)
    {
        _context = context;
    }

    public virtual Task<Item?> GetByIdActiveAsync(int id)
    {
        var item = _context.FirstOrDefaultAsync(i => i.Id == id && i.IsActive);
        return item;
    }

    public virtual async Task<IEnumerable<Item>> GetAllActiveAsync()
    {
        var items = _context.WhereAsync(i => i.IsActive);
        return await items;
    }
}
