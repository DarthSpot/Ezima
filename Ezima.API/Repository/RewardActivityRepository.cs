using Ezima.API.Model;
using Ezima.API.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace Ezima.API.Repository;

public class RewardActivityRepository : IRepository<RewardActivity>
{
    private readonly EzimaContext _context;

    public RewardActivityRepository(EzimaContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RewardActivity?>> FindAll()
    {
        return await _context.RewardActivities.ToListAsync();
    }

    public async Task<RewardActivity?> FindById(int id)
    {
        return await _context.RewardActivities.FindAsync(id);
    }

    public async Task<RewardActivity?> Save(RewardActivity entity)
    {
        var result = await _context.RewardActivities.AddAsync(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> Delete(RewardActivity entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<RewardActivity?> Update(RewardActivity entity)
    {
        throw new NotImplementedException();
    }

    public async Task Clear()
    {
        throw new NotImplementedException();
    }

    public async Task<int> Count()
    {
        throw new NotImplementedException();
    }
}