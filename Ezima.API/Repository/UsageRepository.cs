using Ezima.API.Model;
using Ezima.API.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace Ezima.API.Repository;

public class UsageRepository(EzimaContext context) : IRepository<RewardUsage>
{
    public async Task<IEnumerable<RewardUsage?>> FindAll()
    {
        return await context.RewardUsages.ToListAsync();
    }

    public async Task<RewardUsage?> FindById(int id)
    {
        return await context.RewardUsages
            .Include(x => x.Children)
            .FirstOrDefaultAsync(x => x.Id ==id);
    }

    public async Task<RewardUsage?> Save(RewardUsage entity)
    {
        if (entity == null)
            return null;
        var creationResult = await context.RewardUsages.AddAsync(entity);
        if (creationResult.State == EntityState.Added)
        {
            await context.SaveChangesAsync();
            return creationResult.Entity;
        }

        return null;
    }

    public async Task<bool> Delete(RewardUsage entity)
    {
        var deleteResult = context.RewardUsages.Remove(entity);
        if (deleteResult.State == EntityState.Deleted)
        {
            await context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<bool> DeleteById(int id)
    {
        var entry = await FindById(id);
        if (entry != null)
            return await Delete(entry);
        return false;
    }

    public async Task<RewardUsage?> Update(RewardUsage entity)
    {
        var updateResult = context.RewardUsages.Update(entity);
        if (updateResult.State == EntityState.Modified)
        {
            await context.SaveChangesAsync();
            return updateResult.Entity;
        }
        return null;
    }

    public async Task Clear()
    {
        context.RewardUsages.RemoveRange(context.RewardUsages);
    }

    public async Task<int> Count()
    {
        return await context.RewardUsages.CountAsync();
    }
}