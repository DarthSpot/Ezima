using Ezima.API.Model;
using Ezima.API.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace Ezima.API.Repository;

public class RewardRepository(EzimaContext context) : IRepository<Reward>
{
    public async Task<IEnumerable<Reward?>> FindAll()
    {
        return await context.Rewards.ToListAsync();
    }

    public async Task<Reward?> FindById(int id)
    {
        return await context.Rewards.FindAsync(id);
    }

    public async Task<Reward?> Save(Reward entity)
    {
        if (entity == null)
            return null;
        var saveResult = await context.Rewards.AddAsync(entity);
        if (saveResult.State == EntityState.Added)
        {
            await context.SaveChangesAsync();
            return saveResult.Entity;
        }
        return null;
    }

    public async Task<bool> Delete(Reward entity)
    {
        var deleteResult = context.Rewards.Remove(entity);
        if (deleteResult.State == EntityState.Deleted)
        {
            await context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<bool> DeleteById(int id)
    {
        var entity = await FindById(id);
        if (entity == null)
            return false;
        return await Delete(entity);
    }

    public async Task<Reward?> Update(Reward entity)
    {
        var updateResult = context.Rewards.Update(entity);
        if (updateResult.State == EntityState.Modified)
        {
            await context.SaveChangesAsync();
            return updateResult.Entity;
        }
        return null;
    }

    public async Task Clear()
    {
        context.Rewards.RemoveRange(context.Rewards);
    }

    public async Task<int> Count()
    {
        return context.Rewards.Count();
    }
}