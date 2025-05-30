using Ezima.API.Model;
using Ezima.API.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace Ezima.API.Repository;

public class UserRepository(EzimaContext context) : IRepository<User>
{
    public async Task<IEnumerable<User?>> FindAll()
    {
        return await context.Users.ToListAsync();
    }

    public async Task<User?> FindById(int id)
    {
        return await context.Users.Include(x => x.Children).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User?> Save(User entity)
    {
        if (entity == null)
            return null;
        var user = await context.Users.AddAsync(entity);
        if (user.State != EntityState.Added)
            return null;
        await context.SaveChangesAsync();
        return user.Entity;
    }

    public async Task<bool> Delete(User entity)
    {
        context.Users.Remove(entity);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteById(int id)
    {
        var user = await FindById(id);
        if (user == null)
            return false;
        return await Delete(user);
    }

    public async Task<User?> Update(User entity)
    {
        context.Users.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task Clear()
    {
        context.Users.RemoveRange(context.Users);
    }

    public async Task<int> Count()
    {
        return await context.Users.CountAsync(); 
    }

    public async Task<User?> FindByOAuthId(string oauthId)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.OAuthId == oauthId);
    }
}