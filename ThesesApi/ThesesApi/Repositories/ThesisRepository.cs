using ThesesApi.Data;
using ThesesApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace ThesesApi.Repositories;

public class ThesisRepository : IThesisRepository
{
    private readonly ThesisContext _context;

    public ThesisRepository(ThesisContext context){
        _context = context;
    }

    public async Task<IEnumerable<Thesis>> getAllTheseAsync()
    {
        return await _context.thesis.Include(t => t.MainAuthor)
                                    .ToListAsync();
    }


       
    public async Task<Thesis?> getByIdAsync(long id)
    {
        return await _context.thesis.Include(t => t.MainAuthor)
                                    .Include(o => o.OtherAuthors)
                                    .ThenInclude(tao => tao.Author)
                                    .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task addAsync(Thesis thesis)
    {
        _context.thesis.Add(thesis);
        await _context.SaveChangesAsync();
    }

    public async Task updateAsync(Thesis thesis)
    {
        _context.thesis.Update(thesis);
        await _context.SaveChangesAsync();
    }

    public async Task deleteAsync(Thesis thesis)
    {
        _context.thesis.Remove(thesis);
            await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Thesis>> getAllThesesAsync()
    {
        return await _context.thesis.ToListAsync();
    }

}
