using Ezima.API.Model;
using Ezima.API.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace Ezima.API.Repository;

public class ChildRepository : IRepository<Child>
{
    private readonly EzimaContext _context;

    public ChildRepository(EzimaContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Child?>> FindAll()
    {
        return await _context.Children.ToListAsync();
    }

    public async Task<Child?> FindById(int id)
    {
        return await _context.Children
            .Include(x => x.RewardActivities)
            .Include(x => x.Rewards)
            .Include(x => x.RewardUsages)
            .SingleOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task<List<Child>> FindAllByUserId(int? userId)
    {
        return await _context.Children
            .Where(x => x.Parents.Any(p => p.Id == userId))
            .ToListAsync();
    }

    public async Task<Child?> Save(Child entity)
    {
        var result = await _context.Children.AddAsync(entity);
        await _context.SaveChangesAsync();
        return result.Entity;;
    }

    public async Task<bool> Delete(Child entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteById(int id)
    {
        var child = await _context.Children.FindAsync(id);
        if (child == null)
            return false;
        return await Delete(child);
    }

    public async Task<Child?> Update(Child entity)
    {
        if (entity == null)
            return null;
        
        var result = _context.Update(entity);
        await _context.SaveChangesAsync();
        return result?.Entity;
    }

    public async Task Clear()
    {
        _context.Children.RemoveRange(_context.Children);
    }

    public async Task<int> Count()
    {
        return await _context.Children.CountAsync();
    }
}